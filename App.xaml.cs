using EventManagerClient.AppLayer.UseCases.Events;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.View;
using EventManagerClient.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace EventManagerClient
{
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            // Register repositories
            services.AddSingleton<IEventRepository, EventApiService>();

            // Register use cases
            services.AddSingleton<GetEventsUseCase>();

            // Register view models
            services.AddSingleton<EventsViewModel>();
            services.AddSingleton<UsersViewModel>();


            // Register views
            services.AddSingleton<MainWindow>();  // Register MainWindow
            services.AddSingleton<EventsWindow>(); // If needed, register EventsWindow
            services.AddSingleton<UsersWindow>();


            services.AddTransient<EventsWindow>(provider =>
            {
                var eventsViewModel = provider.GetRequiredService<EventsViewModel>();
                return new EventsWindow(eventsViewModel);
            });

            services.AddTransient<UsersWindow>(provider =>
            {
                var usersViewModel = provider.GetRequiredService<UsersViewModel>();
                return new UsersWindow(usersViewModel);
            });

            // Register HTTP client
            services.AddHttpClient<EventApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080/api");
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create and show MainWindow
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
