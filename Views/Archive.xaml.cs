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
        if (Stack.Children.Count > 1) for (int i = 0; i < 2; i++) Stack.RemoveAt(Stack.Children.Count - 1);
        foreach (KeyValuePair<DateTime, List<Models.Task>> pair in sortedTask)
		{
            Label label = new Label
			{
				Text = pair.Key.ToString("MM/dd/yyyy"),
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Fill,
				BackgroundColor = Colors.AliceBlue,
				Padding = 10
			};
            VerticalStackLayout newStack = new VerticalStackLayout() { Spacing = 10, Margin = 10 };
			foreach (Models.Task task in pair.Value)
			{
				numTasks++;
                FlexLayout flexLayout = new FlexLayout() { Direction = Microsoft.Maui.Layouts.FlexDirection.Row };
                flexLayout.Add(new Label { Text = task.Content });
                flexLayout.Add(new Label { Text = task.DueDate.ToString("MM/dd/yyyy") });
				flexLayout.SetGrow(flexLayout.Children[0], 1);
				newStack.Add(flexLayout);
			}
			Stack.Add(label);
			Stack.Add(newStack);
		}
		numTasks -= ((VerticalStackLayout)Stack.Children[Stack.Children.Count - 1]).Children.Count;
    }
}