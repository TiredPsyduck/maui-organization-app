namespace OrganizationApp.Views;

public partial class Tasks : ContentPage
{
    public List<VerticalStackLayout> tasks { get; set; }

	public Tasks()
	{
		InitializeComponent();
        tasks = new List<VerticalStackLayout>();
    }

    async private void OnSubmit(object sender, EventArgs e)
    {
        if (Picker.SelectedItem == null) return;
        App.MainViewModel.AddProgress(Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]));
        ProgressBar progressBar = (ProgressBar) tasks.ElementAt(Picker.SelectedIndex).Children[1];
        await progressBar.ProgressTo(progressBar.Progress + 0.5, 500, Easing.Linear);
        if (progressBar.Progress == 1.0)
        {
            Stack.Remove(tasks[Picker.SelectedIndex]);
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
        for (int i = 0; i < tasks.Count; i++)
        {
            Stack.Remove(tasks[i]);
            Picker.Items.Remove(Picker.Items[0]);
        }
        tasks = new List<VerticalStackLayout>();
        var newTasks = App.MainViewModel.GetTasks().ToList();
        foreach (Models.Task task in newTasks)
        {
            VerticalStackLayout newStack = new VerticalStackLayout { Spacing = 10 };
            newStack.Add(new Label { Text = task.Content });
            newStack.Add(new ProgressBar { Progress = task.Progress, ProgressColor = Colors.DarkGreen });
            HorizontalStackLayout assignmentDates = new HorizontalStackLayout { Spacing = 20 };
            assignmentDates.Add(new Label { Text = $"Assigned Date: {task.Date.ToString("MM/dd/yyyy")}" });
            assignmentDates.Add(new Label { Text = $"Due Date: {task.DueDate.ToString("MM/dd/yyyy")}" });
            newStack.Add(assignmentDates);
            Picker.Items.Add($"{task.ID.ToString()}: {task.Content}");
            Stack.Add(newStack);
            tasks.Add(newStack);
        }
    }
}