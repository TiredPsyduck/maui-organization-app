namespace OrganizationApp.Views;

public partial class Templates : ContentPage
{
	public List<HorizontalStackLayout> labels { get; set; }

    public Templates()
	{
		InitializeComponent();
		labels = new List<HorizontalStackLayout>();
        DueDate.Date = DateTime.Today.AddDays(1);
    }

	public void UseTemplate(object sender, EventArgs e)
	{
        if (Picker.SelectedItem == null) return;
        int index = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]);
        var templateModel = App.MainViewModel.TemplatesViewModel.Templates[index];
        string template = $"{templateModel.Content} {templateModel.Index.ToString()}";
        var label = (Label) labels[index].Children[1];
        label.Text = (templateModel.Index + 1).ToString();
        App.MainViewModel.AddTask(template, DueDate.Date);
        App.MainViewModel.TemplatesViewModel.UpdateTemplate(index);
	}

    public void RemoveTemplate(object sender, EventArgs e)
    {
        if (Picker.SelectedItem == null) return;
        int index = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]);
        Stack.Remove(labels[index]);
        Picker.Items.Remove(Picker.SelectedItem.ToString());
    }

    protected override void OnAppearing()
    {
        for (int i = labels.Count; i < App.MainViewModel.TemplatesViewModel.Templates.Count; i++)
        {
            var template = new HorizontalStackLayout { Spacing = 20 };
            template.Add(new Label { Text = App.MainViewModel.TemplatesViewModel.Templates[i].Content });
            template.Add(new Label { Text = App.MainViewModel.TemplatesViewModel.Templates[i].Index.ToString() });
            Stack.Add(template);
            labels.Add(template);
            Picker.Items.Add(i.ToString() + ":" + App.MainViewModel.TemplatesViewModel.Templates[i].Content);
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
}