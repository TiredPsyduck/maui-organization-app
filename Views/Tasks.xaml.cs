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
        if (Picker.SelectedItem == null)
        {
            return;
        }
        int selectedIndex = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]);
        App.MainViewModel.AddProgress(selectedIndex);
        await progressBars[selectedIndex].ProgressTo(App.MainViewModel.Tasks[selectedIndex].Progress, 500, Easing.Linear);
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
        for (int i = progressBars.Count; i < App.MainViewModel.Tasks.Count; i++)
        {
            var label = new Label
            {
                Text = App.MainViewModel.Tasks[i].Content
            };
            var progressBar = new ProgressBar
            {
                Progress = App.MainViewModel.Tasks[i].Progress,
                ProgressColor = Colors.DarkGreen
            };
            var assignmentDates = new HorizontalStackLayout
            {
                Spacing = 20
            };
            assignmentDates.Add(new Label
            {
                Text = "Assigned Date: " + App.MainViewModel.Tasks[i].Date.ToString()
            });
            assignmentDates.Add(new Label
            {
                Text = "Due Date: " + App.MainViewModel.Tasks[i].DueDate.ToString()
            });
            Picker.Items.Add(i.ToString() + ":" + App.MainViewModel.Tasks[i].Content);
            Stack.Add(label);
            labels.Add(label);
            Stack.Add(assignmentDates);
            dates.Add(assignmentDates);
            Stack.Add(progressBar);
            progressBars.Add(progressBar);
        }
    }
}