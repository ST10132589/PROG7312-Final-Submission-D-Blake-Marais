//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System;
namespace MunicipalServicesApp.Windows
{
    /// <summary>
    /// Interaction logic for ReportIssues.xaml
    /// </summary>
       //==============================================================[START OF CLASS]==============================================================
    public partial class ReportIssues : Window
    {
        private BarProgress barProgress = new BarProgress(false, false, false, false, false);


        //==============================================================[START OF ReportIssues]==============================================================
        //Initializes the ReportIssues window
        public ReportIssues()
        {
            InitializeComponent();
            MainFrame.Source = new Uri("/MunicipalServicesApp;component/Pages/ReportLocationAndIssuePage.xaml", UriKind.Relative);
        }
        //==============================================================[END OF ReportIssues]==============================================================

        //==============================================================[START OF Window_Closing]==============================================================
        //Closes the application when clicking close
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        //==============================================================[END OF Window_Closing]==============================================================

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

    }
    //==============================================================[END OF CLASS]==============================================================
}
//==============================================================[END OF FILE]==============================================================
