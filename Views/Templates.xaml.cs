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
        int number = App.MainViewModel.TemplatesViewModel.UseTemplate(index, DueDate.Date);
        var label = (Label) labels[Picker.SelectedIndex].Children[1];
        label.Text = number.ToString();
	}

    public void RemoveTemplate(object sender, EventArgs e)
    {
        if (Picker.SelectedItem == null) return;
        int index = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]);
        App.MainViewModel.TemplatesViewModel.DeleteTemplate(index);
        Stack.Remove(labels[Picker.SelectedIndex]);
        labels.RemoveAt(Picker.SelectedIndex);
        Picker.Items.Remove(Picker.SelectedItem.ToString());
    }

    protected override void OnAppearing()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            Stack.Remove(labels[i]);
            Picker.Items.Remove(Picker.Items[0]);
        }
        labels = new List<HorizontalStackLayout>();
        List<Models.Template> templates = App.MainViewModel.TemplatesViewModel.GetTemplates();
        foreach (Models.Template template in templates)
        {
            var newStack = new HorizontalStackLayout { Spacing = 20 };
            newStack.Add(new Label { Text = template.Content });
            newStack.Add(new Label { Text = template.Index.ToString() });
            Stack.Add(newStack);
            labels.Add(newStack);
            Picker.Items.Add($"{template.ID}: {template.Content}");
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