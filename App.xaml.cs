using EventManagerClient.AppLayer.UseCases.Events;
using EventManagerClient.AppLayer.UseCases.Users;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Infastructure.API;
using EventManagerClient.Infastructure.Repos;
using EventManagerClient.Infrastructure.Repositories;
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
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IEventRepository, EventRepository>();

            // Register use cases
            services.AddSingleton<GetEventsUseCase>();
            services.AddSingleton<GetUsersUseCase>();
            services.AddSingleton<DeleteUserUseCase>();
            services.AddSingleton<EditUserUseCase>();
            services.AddSingleton<CreateUserUseCase>();

            // Register view models
            services.AddSingleton<EventsViewModel>();
            services.AddSingleton<UsersViewModel>();

            // Register views
            services.AddSingleton<MainWindow>();
            services.AddSingleton<EventsWindow>();
            services.AddSingleton<UsersWindow>();
            services.AddSingleton<NewUserWindow>();
            services.AddSingleton<EditUserWindow>();

            // Register HTTP clients
            services.AddHttpClient<EventApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080/api/");
            });

            services.AddHttpClient<UserApiService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8080/api/");
            });

            // Register views with injected ViewModels
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

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
