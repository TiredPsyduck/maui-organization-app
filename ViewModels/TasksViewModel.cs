using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace OrganizationApp.ViewModels;

public class TasksViewModel : ObservableObject
{
    public TemplatesViewModel TemplatesViewModel { get; set; } = new TemplatesViewModel();
    public ObservableCollection<Models.Task> Tasks { get; set; } = new ObservableCollection<Models.Task>();

    public void AddTask(String Task, DateTime DueDate)
    {
        Tasks.Add(new Models.Task(Task, DateTime.Today, 0.0, DueDate));
    }

    public void AddProgress(int Index)
    {
        if (Tasks[Index].Progress < 1)
        {
            Tasks[Index].Progress += 0.5;
        }
    }
}