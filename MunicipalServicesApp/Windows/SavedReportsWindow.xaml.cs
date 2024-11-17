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
using System.Windows.Shapes;
using MunicipalServicesApp.Classes;
using MunicipalServicesApp.Pages;

namespace MunicipalServicesApp.Windows
{
    /// <summary>
    /// Interaction logic for SavedReportsWindow.xaml
    /// </summary>
    public partial class SavedReportsWindow : Window
    {
        public SavedReportsWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            if(SavedReports.GetReports().Count == 0)
            {
                MessageBox.Show("No reports have been saved.", "No Reports", MessageBoxButton.OK, MessageBoxImage.Information);
                var newWindow = new MainWindow();
                newWindow.Show();
                this.Hide();
            }
            LoadReports();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        //==============================================================[START OF btnBack_MouseEnter]==============================================================
        //Changes cursor to hand on mouse enter
        private void btnBack_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        //==============================================================[END OF btnBack_MouseEnter]==============================================================

        //==============================================================[START OF btnBack_MouseLeave]==============================================================
        //Resets cursor to arrow on mouse leave
        private void btnBack_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        //==============================================================[END OF btnBack_MouseLeave]==============================================================

        //==============================================================[START OF btnBack_MouseLeftButtonUp]==============================================================
        //Navigates back to the MainWindow and hides the current window
        private void btnBack_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            this.Hide();
        }
        //==============================================================[END OF btnBack_MouseLeftButtonUp]==============================================================

        private void LoadReports()
        {
            List<Report> reports = SavedReports.GetReports();
            for (int i = 0; i < reports.Count; i++)
            {
                string priorityText = GetPriorityText(reports[i].Priority);
                lstSavedReports.Items.Add($"{i + 1}. {reports[i].Description}. Priority: {priorityText}");
            }
        }
        private void btnOrderByPriority_Click(object sender, RoutedEventArgs e)
        {
            if(cmbSortOrder.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a sort order.", "No Sort Order Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var selectedItem = (ComboBoxItem)cmbSortOrder.SelectedItem;
                bool ascending = selectedItem.Tag.ToString() == "Ascending";
                var sortedReports = SavedReports.GetReportsSortedByPriority(ascending);
                lstSavedReports.Items.Clear();
                for (int i = 0; i < sortedReports.Count; i++)
                {
                    string priorityText = GetPriorityText(sortedReports[i].Priority);
                    lstSavedReports.Items.Add($"{i + 1}. {sortedReports[i].Description}. Priority: {priorityText}");
                }
            }
            
        }


        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            if (lstSavedReports.SelectedIndex >= 0)
            {
                List<Report> reports = SavedReports.GetReports();
                Report selectedReport = reports[lstSavedReports.SelectedIndex];

                string priorityText = GetPriorityText(selectedReport.Priority);
                string message = $"Location: {selectedReport.Location}\nIssue Type: {selectedReport.IssueType}\nDescription: {selectedReport.Description}\nPriority: {priorityText}";

                var confirmationWindow = new ReportConfirmationWindow(message, selectedReport.Image);
                confirmationWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a report to view.", "No Report Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string GetPriorityText(int priority)
        {
            switch (priority)
            {
                case 1:
                    return "High";
                case 2:
                    return "Medium";
                case 3:
                    return "Low";
                default:
                    return "Unknown";
            }
        }
    }
}
