//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes.ViewModels;
using MunicipalServicesApp.Classes;
using System.Collections.Generic;
using System.Windows;
using static MunicipalServicesApp.Classes.ServiceRequest;
using System.Linq;

namespace MunicipalServicesApp.Windows
{
//==============================================================[START OF CLASS]============================================================== 
/// <summary>
/// This the class to view the details and the dependcies of a given service request
/// Its like a better pop up essentially
/// </summary>

    public partial class ServiceRequestsDetailsWindow : Window
    {
        public List<Dependency> Dependencies { get; set; }

        public ServiceRequestsDetailsWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// If there are no dependencies, the listbox is hidden and the label is shown
        /// </summary>
        public ServiceRequestsDetailsWindow(ServiceRequestViewModel viewModel, ServiceRequest selectedRequest)
        {
            InitializeComponent();
            DataContext = selectedRequest;
            Dependencies = viewModel.GetDependencies(selectedRequest);
            DependenciesListBox.ItemsSource = Dependencies;
            if (Dependencies.Count == 0)
            {
                DependenciesListBox.Visibility = Visibility.Collapsed;
                DependenciesLabel.Visibility = Visibility.Visible;
            }
        }

        //Closes the window when clicking okay, because i removed the navbar at the top of the window
        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           Window_Closed(sender, e);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            // Get the main window instance
            var mainWindow = Application.Current.Windows.OfType<ServiceRequestsWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                // Clear the selected index
                mainWindow.ServiceRequestsListView.SelectedIndex = -1;
            }
        }
    }
}