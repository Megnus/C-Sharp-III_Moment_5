using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
// Drawing Polylines and Plygons.
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // stores the collection of points for the mutisided shapes
        private PointCollection points = new PointCollection();
        private PointCollection lbxPointCollection = new PointCollection();

        private PointCollection pp = new PointCollection();
        private List<Point> pointList = new List<Point>();
        private Point size, origo;

        // initialize the points of the shapes
        public MainWindow()
        {
            InitializeComponent();
            polyline.Visibility = Visibility.Visible;
            polyline.Points = points;
            lbxPoints.ItemsSource = lbxPointCollection;

            size = new Point(drawCanvas.Width, drawCanvas.Height);
            origo = new Point((int)(size.X / 2), (int)(size.Y / 2));
        }

        // when the clear Button is clicked
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            points.Clear(); // clear the poitns from the collection
            drawCanvas.Children.Clear();
            lbxPointCollection.Clear();
            lbxPoints.Items.Refresh();
            btnSettingsOk_Click(sender, e);
        }

        private void DrawCoordinates(double x1, double y1, double x2, double y2)
        {
            line.Visibility = Visibility.Visible;
            Line myLine = new Line();

            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            myLine.X1 = (int)x1;
            myLine.X2 = (int)x2;
            myLine.Y1 = (int)y1;
            myLine.Y2 = (int)y2;

            myLine.StrokeThickness = 1;
            drawCanvas.Children.Add(myLine);
        }

        private void DrawText(int noOfDevisionsX, int noOfDevisionsY, int intervalValX, int intervalValY)
        {
            double offsetX = (noOfDevisionsY * intervalValY).ToString().Length * 8;
            double offsetY = 35;

            for (int i = 0; i <= noOfDevisionsX; i++)
            {
                double x = offsetX + (size.X - offsetX * 2) / noOfDevisionsX * i;
                Text(x, size.Y - offsetY, (i * intervalValX).ToString(), Colors.Black, Double.NaN);
            }

            int len = (noOfDevisionsY * intervalValY).ToString().Length;
            for (int i = 0; i <= noOfDevisionsY; i++)
            {
                double y = offsetY + (size.Y - offsetY * 2) / noOfDevisionsY * i + 10;
                Text(2, size.Y - y, (i * intervalValY).ToString(), Colors.Black, offsetX  - 5);
            }

            DrawCoordinates(offsetX, 10, offsetX, size.Y - offsetY);
            DrawCoordinates(offsetX, size.Y - offsetY, size.X - 10, size.Y - offsetY);
        }

        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            int noOfDevisionsX = 12;
            int noOfDevisionsY = 10;
            int intervalValX = 1;
            int intervalValY = 100;

            int offsetX = 20;
            int offsetY = 20;

            double x = double.Parse(xCoord.Text);
            double y = double.Parse(yCoord.Text);

            int width = (int)((size.X - offsetX * 2) * x / (intervalValX * noOfDevisionsX)) + offsetX;
            int top = (int)size.Y - (int)((size.Y - offsetY * 2) * y / (intervalValY * noOfDevisionsY)) - offsetY;

            polyline.Visibility = Visibility.Visible;
            polyline.SnapsToDevicePixels = true;
            polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            lbxPointCollection.Add(new Point(x, y));
            CalculatePoint(lbxPointCollection);

            lbxPoints.Items.Refresh();
        }

        private PointCollection CalculatePoint(PointCollection pointCollection)
        {
            PointCollection newPointCollection = new PointCollection();
            points.Clear();
            int noOfDevisionsX = int.Parse(tbxNumDevX.Text);
            int noOfDevisionsY = int.Parse(tbxNumDevY.Text);
            int intervalValX = int.Parse(tbxIntervalValX.Text);
            int intervalValY = int.Parse(tbxIntervalValY.Text);
            int offsetX = 35;
            int offsetY = 35;

            foreach (var p in pointCollection)
            {
                Point pp = new Point();
                pp.X = (int)((size.X - offsetX * 2) * p.X / (intervalValX * noOfDevisionsX)) + offsetX;
                pp.Y = (int)size.Y - (int)((size.Y - offsetY * 2) * p.Y / (intervalValY * noOfDevisionsY)) - offsetY;
                points.Add(pp);
                Debug.WriteLine(p);

                Ellipse ellipse = new Ellipse();
                ellipse.Width = ellipse.Height = 5.0;
                ellipse.Stroke = Brushes.Black;
                ellipse.SetValue(Canvas.LeftProperty, pp.X - 3.0);
                ellipse.SetValue(Canvas.TopProperty, pp.Y - 3.0);
                drawCanvas.Children.Add(ellipse);
            }

            return newPointCollection;
        }

        private void Text(double x, double y, string text, Color color, double width)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.TextAlignment = TextAlignment.Right;
            textBlock.Width = width;
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            drawCanvas.Children.Add(textBlock);
        }

        private void btnSettingsOk_Click(object sender, RoutedEventArgs e)
        {
            drawCanvas.Children.Clear();

            polyline = new Polyline();
            polyline.Visibility = Visibility.Visible;

            polyline.Stroke = System.Windows.Media.Brushes.Black;
            polyline.SnapsToDevicePixels = true;
            polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            polyline.StrokeThickness = 1;
            drawCanvas.Children.Add(polyline);

            polyline.Visibility = Visibility.Visible;
            polyline.Points = points;
            
            int noOfDevisionsX = int.Parse(tbxNumDevX.Text);
            int noOfDevisionsY = int.Parse(tbxNumDevY.Text);
            int intervalValX = int.Parse(tbxIntervalValX.Text);
            int intervalValY = int.Parse(tbxIntervalValY.Text);
            DrawText(noOfDevisionsX, noOfDevisionsY, intervalValX, intervalValY);
            CalculatePoint(lbxPointCollection);
        }
    }
}