﻿namespace OrganizationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");
            builder.Services.AddSingleton<ViewModels.TasksViewModel>(s => ActivatorUtilities.CreateInstance<ViewModels.TasksViewModel>(s, dbPath));
            return builder.Build();
        }
    }
}