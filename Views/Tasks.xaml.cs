namespace OrganizationApp.Views;

public partial class Tasks : ContentPage
{
    public List<ProgressBar> progressBars { get; set; }
    public List<Label> labels { get; set; }
    public List<HorizontalStackLayout> dates { get; set; }

	public Tasks()
	{
		InitializeComponent();
        progressBars = new List<ProgressBar>();
        labels = new List<Label>();
        dates = new List<HorizontalStackLayout>();
    }

    async private void OnSubmit(object sender, EventArgs e)
    {
        if (Picker.SelectedItem == null) return;
        App.MainViewModel.AddProgress(Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]));
        await progressBars[Picker.SelectedIndex].ProgressTo(progressBars[Picker.SelectedIndex].Progress + 0.5, 500, Easing.Linear);
        if (progressBars[Picker.SelectedIndex].Progress == 1.0)
        {
            Stack.Remove(progressBars[Picker.SelectedIndex]);
            Stack.Remove(labels[Picker.SelectedIndex]);
            Stack.Remove(dates[Picker.SelectedIndex]);
            progressBars.RemoveAt(Picker.SelectedIndex);
            labels.RemoveAt(Picker.SelectedIndex);
            dates.RemoveAt(Picker.SelectedIndex);
            Picker.Items.Remove(Picker.SelectedItem.ToString());
        }
    }

    protected override void OnAppearing()
    {
        for (int i = 0; i < progressBars.Count; i++)
        {
            Stack.Remove(progressBars[i]);
            Stack.Remove(labels[i]);
            Stack.Remove(dates[i]);
            Picker.Items.Remove(Picker.Items[0]);
        }
        progressBars = new List<ProgressBar>();
        labels = new List<Label>();
        dates = new List<HorizontalStackLayout>();
        var newTasks = App.MainViewModel.GetTasks();
        for (int i = 0; i < newTasks.Count(); i++)
        {
            var element = newTasks.ElementAt(i);
            var label = new Label { Text = element.Content };
            var progressBar = new ProgressBar { Progress = element.Progress, ProgressColor = Colors.DarkGreen };
            var assignmentDates = new HorizontalStackLayout { Spacing = 20 };
            assignmentDates.Add(new Label { Text = $"Assigned Date: {element.Date.ToString("MM/dd/yyyy")}" });
            assignmentDates.Add(new Label { Text = $"Due Date: {element.DueDate.ToString("MM/dd/yyyy")}" });
            Picker.Items.Add(element.ID.ToString() + ":" + element.Content);
            Stack.Add(label);
            labels.Add(label);
            Stack.Add(assignmentDates);
            dates.Add(assignmentDates);
            Stack.Add(progressBar);
            progressBars.Add(progressBar);
        }
    }
}