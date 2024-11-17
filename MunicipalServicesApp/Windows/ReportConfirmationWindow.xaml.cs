//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
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
using System.Windows.Shapes;

namespace MunicipalServicesApp.Pages
{
    /// <summary>
    /// Interaction logic for ReportConfirmationWindow.xaml
    /// </summary>
    //==============================================================[START OF CLASS]==============================================================
    public partial class ReportConfirmationWindow : Window
    {
        //==============================================================[START OF ReportConfirmationWindow]==============================================================
        //Initializes the ReportConfirmationWindow with report details and the image
        public ReportConfirmationWindow(string details, BitmapImage image)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            txtReportDetails.Text = ConvertPriorityInDetails(details);
            imgReportImage.Source = image;
        }
        //==============================================================[END OF ReportConfirmationWindow]==============================================================

        //==============================================================[START OF btnOk_Click]==============================================================
        //Handles OK button click event, closes the window
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //==============================================================[END OF btnOk_Click]==============================================================

        /// <summary>
        /// Added this with PART THREE so it is readable to the user
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        private string ConvertPriorityInDetails(string details)
        {
            return details.Replace("Priority: 1", "Priority: High")
                          .Replace("Priority: 2", "Priority: Medium")
                          .Replace("Priority: 3", "Priority: Low");
        }

    }
    //==============================================================[END OF CLASS]==============================================================
}
//==============================================================[END OF FILE]==============================================================