//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes.ViewModels;
using MunicipalServicesApp.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MunicipalServicesApp.Windows
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// This is where the graph window is created and displayed.
    /// It uses the GraphViewModel to get the data to display. like the Nodes and Edges
    /// Then it gets the displayFixedGraph method from GraphHelper to draw everything to keep the code
    /// behind clean
    /// </summary>
    public partial class GraphWindow : Window
    {
        private readonly GraphViewModel _viewModel;
        private readonly GraphHelper _graphHelper;

        public GraphWindow(GraphViewModel viewModel)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _viewModel = viewModel;
            _graphHelper = new GraphHelper(GraphCanvas, _viewModel);

            _graphHelper.DisplayFixedGraph();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================