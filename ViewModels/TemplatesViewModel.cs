using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace OrganizationApp.ViewModels;

public class TemplatesViewModel : ObservableObject
{
    public ObservableCollection<Models.Template> Templates { get; set; } = new ObservableCollection<Models.Template>();

    public void AddTemplate(String Template)
    {
        Templates.Add(new Models.Template(Template));
    }

    public void UpdateTemplate(int Index)
    {
        Templates[Index].Index++;
    }
}