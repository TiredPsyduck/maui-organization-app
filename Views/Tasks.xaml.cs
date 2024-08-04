namespace OrganizationApp.Views;

public partial class Tasks : ContentPage
{
    public List<VerticalStackLayout> tasks { get; set; }
    public List<ProgressBar> progressBars { get; set; }

	public Tasks()
	{
		InitializeComponent();
        tasks = new List<VerticalStackLayout>();
        progressBars = new List<ProgressBar>();
    }

    async private void OnSubmit(object sender, EventArgs e)
    {
        if (Picker.SelectedItem == null) return;
        App.MainViewModel.AddProgress(Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]));
        await progressBars[Picker.SelectedIndex].ProgressTo(progressBars[Picker.SelectedIndex].Progress + 0.5, 500, Easing.Linear);
        if (progressBars[Picker.SelectedIndex].Progress == 1.0)
        {
            Stack.Remove(tasks[Picker.SelectedIndex]);
            progressBars.RemoveAt(Picker.SelectedIndex);
            tasks.RemoveAt(Picker.SelectedIndex);
            Picker.Items.Remove(Picker.SelectedItem.ToString());
        }
    }

    private void OnButtonPressed(object sender, EventArgs e)
    {
        Button btnsender = (Button)sender;
        btnsender.BackgroundColor = Colors.Green;
    }

    private void OnButtonReleased(object sender, EventArgs e)
    {
        Button btnsender = (Button)sender;
        btnsender.BackgroundColor = Color.FromArgb("#512BD4");
    }

    protected override void OnAppearing()
    {
        for (int i = 0; i < progressBars.Count; i++)
        {
            Stack.Remove(tasks[i]);
            Picker.Items.Remove(Picker.Items[0]);
        }
        progressBars = new List<ProgressBar>();
        tasks = new List<VerticalStackLayout>();
        var newTasks = App.MainViewModel.GetTasks();
        for (int i = 0; i < newTasks.Count(); i++)
        {
            var element = newTasks.ElementAt(i);
            var newStack = new VerticalStackLayout { Spacing = 10 };
            newStack.Add(new Label { Text = element.Content });
            var progressBar = new ProgressBar { Progress = element.Progress, ProgressColor = Colors.DarkGreen };
            newStack.Add(progressBar);
            var assignmentDates = new HorizontalStackLayout { Spacing = 20 };
            assignmentDates.Add(new Label { Text = $"Assigned Date: {element.Date.ToString("MM/dd/yyyy")}" });
            assignmentDates.Add(new Label { Text = $"Due Date: {element.DueDate.ToString("MM/dd/yyyy")}" });
            newStack.Add(assignmentDates);
            Picker.Items.Add(element.ID.ToString() + ":" + element.Content);
            Stack.Add(newStack);
            progressBars.Add(progressBar);
            tasks.Add(newStack);
        }
    }
}