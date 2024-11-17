//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes.ViewModels;
using System;
using System.Windows;

namespace MunicipalServicesApp.Windows
{
    //==============================================================[START OF CLASS]============================================================== 
    public partial class EventsAndAnnouncementsWindow : Window
    {
        /// <summary>
        /// Constructor for the EventsAndAnnouncementsWindow class. Initialises the Lists
        /// </summary>
        public EventsAndAnnouncementsWindow()
        {
            InitializeComponent();
            DataContext = new EventsAndAnnouncementsWindowViewModel();
        }
        /// <summary>
        /// Closes the application fully when they click the close button
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Uses the methods from the ViewModel to search for events by category and assign scorees and update the lists.
        /// </summary>
        private void SearchByCategory_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EventsAndAnnouncementsWindowViewModel;
            if (viewModel != null && CategoryComboBox.SelectedItem != null)
            {
                viewModel.SearchEventsByCategory(CategoryComboBox.SelectedItem.ToString());
                viewModel.SortEventsByScore();
            }
        }

        /// <summary>
        /// Uses the methods from the ViewModel to search for events by category and assign scorees and update the lists.
        /// </summary>
        private void SearchByDate_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as EventsAndAnnouncementsWindowViewModel;
            if (viewModel != null && StartDatePicker.SelectedDate.HasValue && EndDatePicker.SelectedDate.HasValue)
            {
                viewModel.SearchEventsByDateRange(StartDatePicker.SelectedDate.Value, EndDatePicker.SelectedDate.Value);
                viewModel.SortEventsByScore();
            }
        }

        /// <summary>
        /// Clears the filters and updates the lists.
        /// </summary>
        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
                var viewModel = DataContext as EventsAndAnnouncementsWindowViewModel;
                CategoryComboBox.SelectedItem = null;
                StartDatePicker.SelectedDate = null;
                EndDatePicker.SelectedDate = null;
                viewModel.ClearFilters();
                viewModel.NoEventsMessage = string.Empty;
        }
        
        /// <summary>
        /// Navigates to the Main Menu
        /// </summary>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Hide();
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================