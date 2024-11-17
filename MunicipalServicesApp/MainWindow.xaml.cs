using MunicipalServicesApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
namespace MunicipalServicesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
       //==============================================================[START OF CLASS]============================================================== 
    public partial class MainWindow : Window
    {
        //==============================================================[START OF MainWindow]==============================================================
        //Initializes the MainWindow
        public MainWindow()
        {
            InitializeComponent();
        }
        //==============================================================[END OF MainWindow]==============================================================

        //==============================================================[START OF btnReportIssues_Click]==============================================================
        //Opens the ReportIssues page
        private void btnReportIssues_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Windows.ReportIssues();
            newWindow.Show();
            this.Hide();
        }
        //==============================================================[END OF btnReportIssues_Click]==============================================================

        //==============================================================[START OF Window_Closing]==============================================================
        //Closes the application when clicking close as the application uses a single window and hides ones not in use, this is to prevent the application from running in the background.
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnViewSavedReports_Click(object sender, RoutedEventArgs e)
        {
            if(SavedReports.GetReports().Count == 0)
            {
                MessageBox.Show("No reports have been saved.", "No Reports", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else
            {
                var newWindow = new Windows.SavedReportsWindow();
                newWindow.Show();
                this.Hide();
            }
        }

        private void btnLocalEvents_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Windows.EventsAndAnnouncementsWindow();
            newWindow.Show();
            this.Hide();
        }



        private void btnServiceRequestStatus_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Windows.ServiceRequestsWindow();
            newWindow.Show();
            this.Hide();
        }
        //==============================================================[END OF Window_Closing]==============================================================
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================