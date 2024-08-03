namespace OrganizationApp
{
    public partial class App : Application
    {
        public static ViewModels.TasksViewModel MainViewModel { get; private set; }

        public App()
        {
            InitializeComponent();

            MainViewModel = new(System.IO.Path.Combine(FileSystem.AppDataDirectory, "tasks.db3"));
            MainPage = new AppShell();
        }
    }
}