using EventManagerClient.Presentation.ViewModels;
using System.Windows;

namespace EventManagerClient.Presentation.View
{
    public partial class EventsWindow : Window
    {
        private readonly EventsViewModel _viewModel;


        public EventsWindow(EventsViewModel viewModel)
        {
            InitializeComponent(); 
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.LoadEventsCommand.CanExecute(null))
            {
                _viewModel.LoadEventsCommand.Execute(null);
            }
        }



    }

}