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
        if (Picker.SelectedItem == null)
        {
            return;
        }
        int index = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]); ;
        string template = App.MainViewModel.TemplatesViewModel.Templates[index].Content;
        template += " " + App.MainViewModel.TemplatesViewModel.Templates[index].Index.ToString();
        App.MainViewModel.AddTask(template, DueDate.Date);
        App.MainViewModel.TemplatesViewModel.UpdateTemplate(index);
	}

    public void RemoveTemplate(object sender, EventArgs e)
    {
        if (Picker.SelectedItem == null)
        {
            return;
        }
        int index = Int32.Parse(Picker.SelectedItem.ToString().Split(':', 2)[0]); ;
        Stack.Remove(labels[index]);
        Picker.Items.Remove(Picker.SelectedItem.ToString());
    }

    protected override void OnAppearing()
    {
        for (int i = labels.Count; i < App.MainViewModel.TemplatesViewModel.Templates.Count; i++)
        {
            var template = new HorizontalStackLayout
            {
                Spacing = 20
            };
            template.Add(new Label
            {
                Text = App.MainViewModel.TemplatesViewModel.Templates[i].Content
            });
            template.Add(new Label
            {
                Text = App.MainViewModel.TemplatesViewModel.Templates[i].Index.ToString()
            });
            Stack.Add(template);
            labels.Add(template);
            Picker.Items.Add(i.ToString() + ":" + App.MainViewModel.TemplatesViewModel.Templates[i].Content);
        }
    }
}