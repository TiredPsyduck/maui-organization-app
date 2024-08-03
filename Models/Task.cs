namespace OrganizationApp.Models;

public class Task {
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public double Progress { get; set; }
    public DateTime DueDate { get; set; }

    public Task(string content, DateTime date, double progress, DateTime dueDate)
    {
        Content = content;
        Date = date;
        Progress = progress;
        DueDate = dueDate;
    }
}