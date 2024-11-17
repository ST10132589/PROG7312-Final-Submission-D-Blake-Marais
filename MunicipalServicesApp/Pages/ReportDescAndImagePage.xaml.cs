//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes;
using MunicipalServicesApp.Helpers;
using MunicipalServicesApp.Windows;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MunicipalServicesApp.Pages
{
    /// <summary>
    /// Interaction logic for ReportDescAndImagePage.xaml
    /// Updated this to include the priority for the report
    /// </summary>
    public partial class ReportDescAndImagePage : Page
    {
        private BarProgress barProgress = new BarProgress(true, true, false, false, false);
        private ProgressBarHelper progressBarHelper;
        private Report report;


        /// <summary>
        /// Initialises the report and progbar, 50% progress because its the second page
        /// </summary>
        public ReportDescAndImagePage(Report report)
        {
            this.report = report;
            InitializeComponent();
            progressBarHelper = new ProgressBarHelper(prgBar);
            progressBarHelper.AnimateProgressBar(50);
        }

        //==============================================================[START OF rtxDetailedDescription_LostFocus]==============================================================
        private void rtxDetailedDescription_LostFocus(object sender, RoutedEventArgs e)
        {
            string richText = new TextRange(rtxDetailedDescription.Document.ContentStart, rtxDetailedDescription.Document.ContentEnd).Text.Trim();
            progressBarHelper.ValidateInput(rtxDetailedDescription, () => barProgress.DescSaved, value => barProgress.DescSaved = value, richText);
        }
        //==============================================================[END OF rtxDetailedDescription_LostFocus]==============================================================

        //==============================================================[START OF rtxDetailedDescription_TextChanged]==============================================================
        private void rtxDetailedDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            string richText = new TextRange(rtxDetailedDescription.Document.ContentStart, rtxDetailedDescription.Document.ContentEnd).Text.Trim();
            if (string.IsNullOrEmpty(richText) && barProgress.DescSaved)
            {
                progressBarHelper.DecreaseProgBar();
                barProgress.DescSaved = false;
            }
        }
        //==============================================================[END OF rtxDetailedDescription_TextChanged]==============================================================

        //==============================================================[START OF rtxDetailedDescription_PreviewKeyDown]==============================================================
        private void rtxDetailedDescription_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                string richText = new TextRange(rtxDetailedDescription.Document.ContentStart, rtxDetailedDescription.Document.ContentEnd).Text.Trim();
                progressBarHelper.ValidateInput(rtxDetailedDescription, () => barProgress.DescSaved, value => barProgress.DescSaved = value, richText);
            }
        }
        //==============================================================[END OF rtxDetailedDescription_PreviewKeyDown]==============================================================

        //==============================================================[START OF btnAttachFile_Click]==============================================================
        //Updated this to hopefully not crash, and also now saves the image as a bitmap instead of just the path.
        private void btnAttachFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF)|*.BMP;*.JPG;*.JPEG;*.PNG;*.GIF|All files (*.*)|*.*";
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filename, UriKind.Absolute);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                imgPreview.Source = bitmap;

                report.Image = bitmap; 

                progressBarHelper.IncreaseProgBar();
                barProgress.ImgSaved = true;
                btnAttachFile.BorderBrush = (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));
            }
        }

        //==============================================================[END OF btnAttachFile_Click]==============================================================

        //==============================================================[START OF cmbPriority_SelectionChanged]==============================================================
        /// <summary>
        /// Saves the priority of the report and updated the progress bar
        /// </summary>
        private void cmbPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPriority.SelectedItem is ComboBoxItem selectedItem)
            {
                report.Priority = int.Parse(selectedItem.Tag.ToString());
                progressBarHelper.IncreaseProgBar();
                barProgress.PrioritySaved = true;
            }
            else
            {
                progressBarHelper.DecreaseProgBar();
                barProgress.PrioritySaved = false;
            }
        }
        //==============================================================[END OF cmbPriority_SelectionChanged]==============================================================


        //==============================================================[START OF btnSubmitReport_Click]==============================================================
        //Submits the report if all fields are valid, shows confirmation window and returns to the main window
        private void btnSubmitReport_Click(object sender, RoutedEventArgs e)
        {
            if (barProgress.LocSaved && barProgress.IssueSaved && barProgress.DescSaved && barProgress.ImgSaved && barProgress.PrioritySaved)
            {
                report.Description = new TextRange(rtxDetailedDescription.Document.ContentStart, rtxDetailedDescription.Document.ContentEnd).Text.Trim();

                SavedReports.AddReport(report);

                string message = $"Location: {report.Location}\nIssue Type: {report.IssueType}\nDescription: {report.Description} \nPriority: {report.Priority}" ;

                var confirmationWindow = new ReportConfirmationWindow(message, report.Image);
                confirmationWindow.ShowDialog();


                var mainWindow = new MainWindow();
                mainWindow.Show();

                Window.GetWindow(this).Hide();

            }
            else
            {
                // Highlight the invalid fields
                if (!barProgress.DescSaved)
                {
                    rtxDetailedDescription.BorderBrush = Brushes.Red;
                }
                if (!barProgress.ImgSaved)
                {
                    btnAttachFile.BorderBrush = Brushes.Red;
                }
            }
        }


        //==============================================================[END OF btnSubmitReport_Click]==============================================================

    }
}
//==============================================================[END OF FILE]==============================================================
