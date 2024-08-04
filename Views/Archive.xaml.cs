namespace OrganizationApp.Views;

public partial class Archive : ContentPage
{
	public int numTasks;

	public Archive()
	{
		InitializeComponent();
		numTasks = 0;
	}

	protected override void OnAppearing()
	{
		SortedDictionary<DateTime, List<Models.Task>> sortedTask = App.MainViewModel.SortTasksByDate(numTasks);
		foreach (var pair in sortedTask)
		{
			numTasks++;
			Label label = new Label { Text = pair.Key.ToString() };
			var newStack = new VerticalStackLayout() { Spacing = 5 };
			foreach (var task in pair.Value)
			{
				newStack.Add(new Label { Text = task.Content });
				newStack.Add(new Label { Text = task.DueDate.ToString() });
			}
			Stack.Add(label);
			Stack.Add(newStack);
		}
    }
}