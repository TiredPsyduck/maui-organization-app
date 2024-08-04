using SQLite;

namespace OrganizationApp.Models;

[Table("templates")]
public class Template
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Content { get; set; }
    public int Index { get; set; }
}