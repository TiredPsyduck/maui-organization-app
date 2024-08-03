namespace OrganizationApp.Models;

public class Template
{
    public string Content { get; set; }
    public int Index { get; set; }

    public Template(string content)
    {
        Content = content;
        Index = 1;
    }
}