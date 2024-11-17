//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using MunicipalServicesApp.Classes.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MunicipalServicesApp.Helpers
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// Class to draw the graph on GraphWindow.
    /// </summary>
    public class GraphHelper
    {
        /// <summary>
        /// Variable declarations
        /// </summary>
        private readonly Canvas _canvas;
        private readonly GraphViewModel _viewModel;
        private const int NodeWidth = 200;
        private const int NodeHeight = 60;
        private const int VerticalSpacing = 120;
        private const int HorizontalSpacing = 250;

        public GraphHelper(Canvas canvas, GraphViewModel viewModel)
        {
            _canvas = canvas;
            _viewModel = viewModel;
        }
        // Method to display a fixed graph with 20 nodes in a row and dependencies below
        public void DisplayFixedGraph(double startX = 200, double startY = 80)
        {
            var nodePositions = new Dictionary<int, Point>();
            var rowDependencyCounts = new Dictionary<int, int>();

            // Draw main row of nodes (1-20)
            for (int i = 1; i <= 20; i++)
            {
                int x = (int)(startX + (i - 1) * HorizontalSpacing);
                int y = (int)startY;

                var position = new Point(x, y);
                nodePositions[i] = position;

                DrawNode(i, position); // Draw the node

                // Initialize dependency count for the row
                rowDependencyCounts[i] = 0;
            }

            // Draw dependencies below each node
            foreach (var node in nodePositions.Keys)
            {
                int dependencyCount = _viewModel.GetDependencyCount(node);
                double depY = nodePositions[node].Y + VerticalSpacing;

                foreach (var dependency in _viewModel.GetDependencies(node))
                {
                    var depPosition = new Point(nodePositions[node].X, depY);

                    DrawNode(dependency, depPosition); // Draw dependency node
                    DrawEdge(
                        new Point(nodePositions[node].X, nodePositions[node].Y + NodeHeight / 2),
                        new Point(depPosition.X, depPosition.Y - NodeHeight / 2)
                    ); // Connect with arrow

                    depY += VerticalSpacing; // Move down for the next dependency

                    // Increment dependency count for the row
                    rowDependencyCounts[node]++;
                }
            }

            // Adds text below the bottom node in each row indicating the number of dependencies
            foreach (var node in nodePositions.Keys)
            {
                int dependencyCount = rowDependencyCounts[node];
                string dependencyText = $"Dependencies: {dependencyCount}";

                var textBlock = new TextBlock
                {
                    Text = dependencyText,
                    Foreground = Brushes.Black,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                double textX = nodePositions[node].X;
                double textY = nodePositions[node].Y + (dependencyCount + 1) * VerticalSpacing;

                Canvas.SetLeft(textBlock, textX - 50); // Adjust position as needed
                Canvas.SetTop(textBlock, textY);
                _canvas.Children.Add(textBlock);
            }

            // Adjust the canvas size to fit all nodes
            _canvas.Width = startX + 20 * HorizontalSpacing;
            _canvas.Height = startY + 20 * VerticalSpacing;
        }

        /// <summary>
        /// This populates each node (SR) with the title and status of the service request.
        /// </summary>
        private void DrawNode(int node, Point position)
        {
            // Get the title and status of the service request
            string title = _viewModel.GetServiceRequestTitle(node);
            string status = _viewModel.GetServiceRequestStatus(node);

            // Determine the fill color based on the status
            Brush fillColor;
            switch (status)
            {
                case "Closed":
                    fillColor = Brushes.Blue;
                    break;
                case "In Progress":
                    fillColor = Brushes.Red;
                    break;
                case "Open":
                    fillColor = Brushes.Green;
                    break;
                default:
                    fillColor = Brushes.LightGray; // Default color for unknown status
                    break;
            }

            // Draw node as a rectangle
            var rectangle = new Rectangle
            {
                Width = NodeWidth,
                Height = NodeHeight,
                Fill = fillColor,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.SetLeft(rectangle, position.X - NodeWidth / 2); // Center the rectangle
            Canvas.SetTop(rectangle, position.Y - NodeHeight / 2); // Center the rectangle
            _canvas.Children.Add(rectangle);

            // Draw node label
            var textBlock = new TextBlock
            {
                Text = title,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                Width = NodeWidth - 20, // Slightly less than rectangle width
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            Canvas.SetLeft(textBlock, position.X - NodeWidth / 2 + 10); // Adjust position
            Canvas.SetTop(textBlock, position.Y - NodeHeight / 2 + 10);
            _canvas.Children.Add(textBlock);
        }

        /// <summary>
        /// used in line 66 to draw the arrow connecting the nodes.
        /// </summary>
        private void DrawEdge(Point from, Point to)
        {
            // Create a path for the arrow
            var path = new Path
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };

            // Create the geometry for the arrow
            var geometry = new PathGeometry();
            var figure = new PathFigure { StartPoint = from };
            var lineSegment = new LineSegment { Point = to };
            figure.Segments.Add(lineSegment);

            // Create the arrowhead
            var arrowHead = new PathFigure { StartPoint = to };
            const int arrowSize = 5;
            const double arrowAngle = Math.PI / 6; // 30 degrees

            var arrowPoint1 = new Point(
                to.X - arrowSize * Math.Cos(arrowAngle),
                to.Y - arrowSize * Math.Sin(arrowAngle)
            );
            var arrowPoint2 = new Point(
                to.X + arrowSize * Math.Cos(arrowAngle),
                to.Y - arrowSize * Math.Sin(arrowAngle)
            );

            arrowHead.Segments.Add(new LineSegment { Point = arrowPoint1 });
            arrowHead.Segments.Add(new LineSegment { Point = arrowPoint2 });
            arrowHead.Segments.Add(new LineSegment { Point = to });

            geometry.Figures.Add(figure);
            geometry.Figures.Add(arrowHead);

            path.Data = geometry;
            _canvas.Children.Add(path);
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================
