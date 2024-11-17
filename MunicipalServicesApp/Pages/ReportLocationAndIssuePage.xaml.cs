//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes;
using MunicipalServicesApp.Helpers;
using MunicipalServicesApp.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MunicipalServicesApp.Pages
{
    /// <summary>
    /// Interaction logic for ReportIssuesLocationAndIssuePage.xaml
    /// </summary>
    public partial class ReportIssuesLocationAndIssuePage : Page
    {
        private BarProgress barProgress = new BarProgress(false, false, false, false,false);
        private ProgressBarHelper progressBarHelper;

        public ReportIssuesLocationAndIssuePage()
        {
            InitializeComponent();
            progressBarHelper = new ProgressBarHelper(prgBar);
        }

        //==============================================================[START OF txtLocationInput_LostFocus]==============================================================
        private void txtLocationInput_LostFocus(object sender, RoutedEventArgs e)
        {
            progressBarHelper.ValidateInput(txtLocationInput, () => barProgress.LocSaved, value => barProgress.LocSaved = value);
            cmbIssueType.Focus();
        }
        //==============================================================[END OF txtLocationInput_LostFocus]==============================================================

        //==============================================================[START OF txtLocationInput_TextChanged]==============================================================
        //Removes any special characters from the location input as you type and updates the progress bar when done
        private void txtLocationInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Regex regex = new Regex(@"[^a-zA-Z0-9\s.,!?;:'""-]");
            int originalCursorPos = textBox.SelectionStart;
            int originalLength = textBox.Text.Length;
            string newText = regex.Replace(textBox.Text, "");

            if (newText != textBox.Text)
            {
                textBox.Text = newText;
                int differenceInLength = originalLength - newText.Length;
                textBox.SelectionStart = Math.Max(0, originalCursorPos - differenceInLength);
            }

            if (string.IsNullOrEmpty(txtLocationInput.Text) && barProgress.LocSaved)
            {
                progressBarHelper.DecreaseProgBar();
                barProgress.LocSaved = false;
            }
        }
        //==============================================================[END OF txtLocationInput_TextChanged]==============================================================

        //==============================================================[START OF btnClearSelection_Click]==============================================================
        private void btnClearSelection_Click(object sender, RoutedEventArgs e)
        {
            cmbIssueType.SelectedIndex = -1;
        }
        //==============================================================[END OF btnClearSelection_Click]==============================================================

        //==============================================================[START OF cmbIssueType_SelectionChanged]==============================================================
        private void cmbIssueType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbIssueType.SelectedIndex != -1)
            {
                rectComboBoxBorder.Visibility = Visibility.Collapsed;
                if (!barProgress.IssueSaved)
                {
                    progressBarHelper.IncreaseProgBar();
                    barProgress.IssueSaved = true;
                }
            }
            else
            {
                rectComboBoxBorder.Visibility = Visibility.Visible;
                if (barProgress.IssueSaved)
                {
                    progressBarHelper.DecreaseProgBar();
                    barProgress.IssueSaved = false;
                }
            }
        }
        //==============================================================[END OF cmbIssueType_SelectionChanged]==============================================================

        //==============================================================[START OF txtLocationInput_KeyDown]==============================================================
        //If you click enter on the location input, it will validate the input and move the focus to the issue type combo box
        private void txtLocationInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                progressBarHelper.ValidateInput(txtLocationInput, () => barProgress.LocSaved, value => barProgress.LocSaved = value);
                cmbIssueType.Focus();
            }
        }
        //==============================================================[END OF txtLocationInput_KeyDown]==============================================================

        //==============================================================[START OF btnSubmitReport_Click]==============================================================
        /// <summary>
        /// Creates a new report and sends it to the next page if the location and issue type are saved, else it shows the errors (missing inputs)
        /// </summary>
        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (barProgress.LocSaved == true && barProgress.IssueSaved == true)
            {
                var report = new Report
                {
                    Location = txtLocationInput.Text,
                    IssueType = (cmbIssueType.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Description = "",
                    Image = null
                };
                ReportDescAndImagePage nextPage = new ReportDescAndImagePage(report);
                NavigationService.Navigate(nextPage);
            }
            else
            {
                if (!barProgress.LocSaved)
                {
                    txtLocationInput.BorderBrush = Brushes.Red;
                }
                if (!barProgress.IssueSaved || cmbIssueType.SelectedIndex == -1)
                {
                    rectComboBoxBorder.Visibility = Visibility.Visible;
                }
            }
        }
        //==============================================================[END OF btnSubmitReport_Click]==============================================================
    }
}
//==============================================================[END OF FILE]==============================================================
