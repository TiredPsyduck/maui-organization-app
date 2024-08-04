using SQLite;

namespace OrganizationApp.ViewModels;

public class TemplatesViewModel
{
    private SQLiteConnection conn;
    
    public TemplatesViewModel(string dbPath)
    {
        conn = new SQLiteConnection(dbPath);
        conn.CreateTable<Models.Template>();
    }

    public void AddTemplate(String Content)
    {
        conn.Insert(new Models.Template { Content = Content, Index = 1 });
    }

    public int UseTemplate(int ID, DateTime dueDate)
    {
        Models.Template template = (from t in conn.Table<Models.Template>() where t.ID == ID select t).FirstOrDefault();
        App.MainViewModel.AddTask($"{template.Content} {template.Index}", dueDate);
        template.Index++;
        conn.Update(template);
        return template.Index;
    }

    public void DeleteTemplate(int ID)
    {
        conn.Delete<Models.Template>(ID);
    }

    public List<Models.Template> GetTemplates()
    {
        return conn.Table<Models.Template>().ToList();
    }
}