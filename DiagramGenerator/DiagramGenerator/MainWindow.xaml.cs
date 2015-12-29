using System;
// Drawing Polylines and Plygons.
using System.Windows;
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
        private PointCollection pp = new PointCollection();

        // initialize the points of the shapes
        public MainWindow()
        {
            InitializeComponent();

            polyline.Points = points; // assing Plyline points
            polygon.Points = points; // assing Plygon points
            filledPolygon.Points = points; // assing filled Plygon points
        }

        // adds a new point when the user clicks on the canvas
        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //add point to collection
            points.Add(e.GetPosition(drawCanvas));
        }

        // when the clear Button is clicked
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            points.Clear(); // clear the poitns from the collection
        }

        // when the user selects the Polyline
        private void lineRadio_Checked(object sender, RoutedEventArgs e)
        {
            // Polyline is visible, the other two are not
            polyline.Visibility = Visibility.Visible;
            polygon.Visibility = Visibility.Collapsed;
            filledPolygon.Visibility = Visibility.Collapsed;
        }

        // when the user selects the Polygon
        private void polygonRadio_Checked(object sender, RoutedEventArgs e)
        {
            // Polygon is visible, the other two are not
            polyline.Visibility = Visibility.Collapsed;
            polygon.Visibility = Visibility.Visible;
            filledPolygon.Visibility = Visibility.Collapsed;
        }

        // when the user selects the filled Polygon
        private void filledPolygonRadio_Checked(object sender, RoutedEventArgs e)
        {
            // filled Polygon is visible, the other two are not
            polygon.Visibility = Visibility.Collapsed;
            polygon.Visibility = Visibility.Collapsed;
            filledPolygon.Visibility = Visibility.Visible;
        }

        private void DrawCoordinates(int x1, int y1, int x2, int y2)
        {
            line.Visibility = Visibility.Visible;
            Line myLine = new Line();

            myLine.Stroke = System.Windows.Media.Brushes.Black;
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            myLine.X1 = x1;
            myLine.X2 = x2;
            myLine.Y1 = y1;
            myLine.Y2 = y2;

            myLine.StrokeThickness = 1;
            drawCanvas.Children.Add(myLine);
        }

        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            polyline.Visibility = Visibility.Visible;
            Random r = new Random();
            int i = r.Next(0, 100);
            int j = r.Next(0, 100);
            points.Add(new Point(i, j));
            DrawCoordinates(240, 0, 240, 800);
            DrawCoordinates(0, 240, 500, 240);
        }
    }
}