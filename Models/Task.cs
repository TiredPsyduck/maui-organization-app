using SQLite;

namespace OrganizationApp.Models;

[Table("tasks")]
public class Task {
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public double Progress { get; set; }
    public DateTime DueDate { get; set; }
}