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
        int selectedIndex = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]) - 1;
        App.MainViewModel.AddProgress(selectedIndex + 1);
        await progressBars[selectedIndex].ProgressTo(progressBars[selectedIndex].Progress + 0.5, 500, Easing.Linear);
        if (progressBars[selectedIndex].Progress == 1.0)
        {
            Stack.Remove(progressBars[selectedIndex]);
            Stack.Remove(labels[selectedIndex]);
            Stack.Remove(dates[selectedIndex]);
            Picker.Items.Remove(Picker.SelectedItem.ToString());
        }
    }

    protected override void OnAppearing()
    {
        var newTasks = App.MainViewModel.GetTasks(progressBars.Count);
        for (int i = 0; i < newTasks.Count(); i++)
        {
            var element = newTasks.ElementAt(i);
            var label = new Label { Text = element.Content };
            var progressBar = new ProgressBar { Progress = element.Progress, ProgressColor = Colors.DarkGreen };
            var assignmentDates = new HorizontalStackLayout { Spacing = 20 };
            assignmentDates.Add(new Label { Text = $"Assigned Date: {element.Date.ToString()}" });
            assignmentDates.Add(new Label { Text = $"Due Date: {element.DueDate.ToString()}" });
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