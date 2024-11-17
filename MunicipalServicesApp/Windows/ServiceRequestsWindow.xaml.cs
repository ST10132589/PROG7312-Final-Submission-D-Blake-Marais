//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes.ViewModels;
using MunicipalServicesApp.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System;

namespace MunicipalServicesApp.Windows
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// This is the majority of part three of the project. This class is responsible for the Service Requests.
    /// Here you can view details of a selected request, reorder the requests, search for a request by ID, and view a graph of the requests.
    /// All using fancy data structures as discusses in the ServiceRequestViewModel class.
    /// </summary>
    public partial class ServiceRequestsWindow : Window
    {
        public ServiceRequestsWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Opens up the details window for the selected service request.
        /// Is called on search and when selecting a request from the list and pressing the view details button.
        /// </summary>
        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedRequest = (ServiceRequest)ServiceRequestsListView.SelectedItem;
            if (selectedRequest != null)
            {
                var viewModel = (ServiceRequestViewModel)DataContext;
                var detailsWindow = new ServiceRequestsDetailsWindow(viewModel, selectedRequest);
                detailsWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a service request to view details.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        /// <summary>
        /// Reordering of the list using the viewmodel
        /// </summary>
        private void ReorderButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ServiceRequestViewModel)DataContext;
            var selectedOrder = (ComboBoxItem)SearchOptionsComboBox.SelectedItem;
            if (selectedOrder != null)
            {
                viewModel.ReorderServiceRequests(selectedOrder.Content.ToString());
            }
        }

        /// <summary>
        /// Tries to find a service request by ID and opens the details window for it if it exists.
        /// if not the user is notified
        /// </summary>

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ServiceRequestViewModel)DataContext;
            if (int.TryParse(SearchTextBox.Text, out int requestId))
            {
                var result = viewModel.SearchById(requestId);
                if (result != null)
                {
                    ServiceRequestsListView.SelectedItem = result;
                    ServiceRequestsListView.ScrollIntoView(result);

                    // Open the details window for the found request
                    var detailsWindow = new ServiceRequestsDetailsWindow(viewModel, result);
                    detailsWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Service request not found.", "Search Result", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid request ID.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Navigates to the Graph Window
        /// </summary>
        private void ViewGraphButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ServiceRequestViewModel)DataContext;
            GraphWindow graphWindow = new GraphWindow(viewModel.GetGraphViewModel());
            graphWindow.Show();
        }
        //Navigates back to the main window
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================