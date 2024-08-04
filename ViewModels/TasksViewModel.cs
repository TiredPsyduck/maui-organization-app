using SQLite;

namespace OrganizationApp.ViewModels;

public class TasksViewModel
{
    public TemplatesViewModel TemplatesViewModel { get; set; }
    private SQLiteConnection conn;

    public TasksViewModel(string dbPath)
    {
        TemplatesViewModel = new TemplatesViewModel(dbPath);
        conn = new SQLiteConnection(dbPath);
        conn.CreateTable<Models.Task>();
    }

    public void AddTask(string content, DateTime dueDate)
    {
        conn.Insert(new Models.Task { Content = content, Date = DateTime.Today, Progress = 0.0, DueDate = dueDate });
    }

    public void AddProgress(int ID)
    {
        Models.Task task = (from t in conn.Table<Models.Task>() where t.ID == ID select t).FirstOrDefault();
        if (task.Progress < 1) task.Progress += 0.5;
        conn.Update(task);
    }

    public SQLite.TableQuery<Models.Task> GetTasks()
    {
        return from t in conn.Table<Models.Task>() where t.Progress != 1.0 select t;
    }

    public SortedDictionary<DateTime, List<Models.Task>> SortTasksByDate(int index)
    {
        List<DateTime> allTasks = (from t in conn.Table<Models.Task>() select t.Date).Distinct().OrderBy(date => date).ToList();
        SortedDictionary<DateTime, List<Models.Task>> sortedTasks = new();
        foreach (DateTime date in allTasks.GetRange(index, allTasks.Count - index))
        {
            List<Models.Task> tasksOnDate = (from t in conn.Table<Models.Task>() where t.Date == date select t).ToList();
            sortedTasks.Add(date, tasksOnDate);
        }
        return sortedTasks;
    }
}