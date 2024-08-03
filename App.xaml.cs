namespace OrganizationApp
{
    public partial class App : Application
    {
        public static ViewModels.TasksViewModel MainViewModel { get; private set; } = new();

        public App()
        {
            InitializeComponent();

            MainViewModel = new();
            MainPage = new AppShell();
        }
    }
}