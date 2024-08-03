using SQLite;

namespace OrganizationApp.ViewModels;

public class TasksViewModel
{
    public TemplatesViewModel TemplatesViewModel { get; set; } = new TemplatesViewModel();
    private SQLiteConnection conn;

    public TasksViewModel(string dbPath)
    {
        conn = new SQLiteConnection(dbPath);
        conn.CreateTable<Models.Task>();
    }

    public void AddTask(string content, DateTime dueDate)
    {
        conn.Insert(new Models.Task { Content = content, Date = DateTime.Today, Progress = 0.0, DueDate = dueDate });
    }

    public void AddProgress(int ID)
    {
        var task = (from t in conn.Table<Models.Task>() where t.ID == ID select t).FirstOrDefault();
        if (task.Progress < 1) task.Progress += 0.5;
        conn.Update(task);
    }

    public SQLite.TableQuery<Models.Task> GetTasks(int ID)
    {
        return from t in conn.Table<Models.Task>() where t.ID > ID select t;
    }
}
