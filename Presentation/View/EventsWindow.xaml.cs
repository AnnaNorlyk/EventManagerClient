using EventManagerClient.Presentation.ViewModels;
using System.Windows;

namespace EventManagerClient.Presentation.View
{
    public partial class EventsWindow : Window
    {
        public EventsWindow(EventsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }


    }

}