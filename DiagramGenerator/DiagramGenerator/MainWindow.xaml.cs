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
            polyline.Points = points; // assing Plyline points
            //polygon.Points = points; // assing Plygon points
            //filledPolygon.Points = points; // assing filled Plygon points

            lbxPoints.ItemsSource = lbxPointCollection;

            size = new Point(drawCanvas.Width, drawCanvas.Height);
            origo = new Point((int)(size.X / 2), (int)(size.Y / 2));

           /*DrawCoordinates(origo.X, 0, origo.X, size.Y);
            DrawCoordinates(0, origo.Y, size.X, origo.Y);
            Debug.WriteLine(size.X);
            Debug.WriteLine(size.Y);*/
        }

        // when the clear Button is clicked
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            points.Clear(); // clear the poitns from the collection
            drawCanvas.Children.Clear();
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
            double offsetX = 35;
            for (int i = 0; i <= noOfDevisionsX; i++)
            {
                double x = offsetX + (size.X - offsetX * 2) / noOfDevisionsX * i;
                Text(x, size.Y - offsetX, (i * intervalValX).ToString(), Colors.Black);
            }

            double offsetY = 35;
            for (int i = 0; i <= noOfDevisionsY; i++)
            {
                double y = offsetY + (size.Y - offsetY * 2) / noOfDevisionsY * i;
                Text(5, size.Y - y, (i * intervalValY).ToString(), Colors.Black);
            }

            DrawCoordinates(offsetX, offsetY, offsetX, size.Y - offsetY);
            DrawCoordinates(offsetX, size.Y - offsetY, size.X - offsetX, size.Y - offsetY);
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

            Ellipse ellipse = new Ellipse();
            ellipse.Width = ellipse.Height = 5.0;
            ellipse.Stroke = Brushes.Black;
            ellipse.SetValue(Canvas.LeftProperty, width - 3.0);
            ellipse.SetValue(Canvas.TopProperty, top - 3.0);

            polyline.Visibility = Visibility.Visible;
            polyline.SnapsToDevicePixels = true;
            polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            lbxPointCollection.Add(new Point(x, y));

            CalculatePoint(lbxPointCollection);
            //polyline.Points = points;

            lbxPoints.Items.Refresh();

            drawCanvas.Children.Add(ellipse);
            //DrawCoordinates(origo.X, 0, origo.X, size.Y);
            //DrawCoordinates(0, origo.Y, size.X, origo.Y);
            //DrawText();
        }

        private PointCollection CalculatePoint(PointCollection pointCollection)
        {
            PointCollection newPointCollection = new PointCollection();
            points.Clear();
            int noOfDevisionsX = int.Parse(tbxNumDevX.Text);//12;
            int noOfDevisionsY = int.Parse(tbxNumDevY.Text);//10;
            int intervalValX = int.Parse(tbxIntervalValX.Text);//1;
            int intervalValY = int.Parse(tbxIntervalValY.Text);//100;
            int offsetX = 20;
            int offsetY = 20;

            foreach (var p in pointCollection)
            {
                Point pp = new Point();
                pp.X = (int)((size.X - offsetX * 2) * p.X / (intervalValX * noOfDevisionsX)) + offsetX;
                pp.Y = (int)size.Y - (int)((size.Y - offsetY * 2) * p.Y / (intervalValY * noOfDevisionsY)) - offsetY;
                points.Add(pp);
                Debug.WriteLine(p);
            }

            return newPointCollection;
        }

        private void Text(double x, double y, string text, Color color)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            drawCanvas.Children.Add(textBlock);
        }

        private void btnSettingsOk_Click(object sender, RoutedEventArgs e)
        {
            //points.Clear(); // clear the poitns from the collection
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