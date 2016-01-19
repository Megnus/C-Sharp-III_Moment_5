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


/* TODO
 *      -- Spara data till xml-fil --
 *      a) Kolla hur man sparar ett objekt till xml
 *      b) Fixa ett Dataobjekt: settings + points
 * x    c) Se till att data sparas till detta objekt och att projektet använder objetet
 *      d) Spara detta objekt 
 *      e) Öppna detta objekt och ladda in data samt sätt groubox till enable/disable
 * x    f) Kolla hur man sorterar data baserat på x i point
 *          pointsOfList = pointsOfList.OrderByDescending(p => p.X).ThenBy(p=>p.y).ToList();
 *      g) Ändra färger på labels osv. 
 *      h) Dela upp i flera klasser bla. en calc-class.
 *      i) Skriv kommentarer och namn och datum på klasserna.
 *      j) Skicka in lösningen.
*/

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
        //removing
        //private Point size, origo;

        private Polyline polylinex;

        
        // new
        private DiagramData diagramData;

        private CanvasHandler canvasHandler;


        // initialize the points of the shapes
        public MainWindow()
        {
            InitializeComponent();

            /* removing
            polyline = new Polyline();
            polyline.Visibility = Visibility.Visible;
            polyline.Stroke = System.Windows.Media.Brushes.Red;
            polyline.SnapsToDevicePixels = true;
            polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            polyline.StrokeThickness = 1;
            drawCanvas.Children.Add(polyline);
            polyline.Visibility = Visibility.Visible;
            polyline.Points = points;
            */
            //polyline.Visibility = Visibility.Visible;
            //polyline.Points = points;

            diagramData = new DiagramData();

            diagramData.CanvasWidth = drawCanvas.Width;
            diagramData.CanvasHeight = drawCanvas.Height;

            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();

            polyline.Points = diagramData.canvasPoints;
            //diagramData.canvasPoints = polyline.Points;



            canvasHandler = new CanvasHandler(drawCanvas, ref diagramData);
            canvasHandler.CreatePolyLine(ref polyline);

            // removing
            //lbxPoints.ItemsSource = lbxPointCollection;

            /* removing
            size = new Point(drawCanvas.Width, drawCanvas.Height);
            origo = new Point((int)(size.X / 2), (int)(size.Y / 2));
            */
            
            WriteXML();
        }

        // when the clear Button is clicked
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            points.Clear(); // clear the poitns from the collection
            drawCanvas.Children.Clear();
            lbxPointCollection.Clear();
            lbxPoints.Items.Refresh();
            btnSettingsOk_Click(sender, e);
            grbSettings.IsEnabled = true;
            grbAddPoint.IsEnabled = false;
            txbXcoordinate.Text = string.Empty;
            txbYcoordinate.Text = string.Empty;
        }

        // removing
        //private void DrawCoordinates(double x1, double y1, double x2, double y2)
        //{
        //    Line myLine = new Line();
        //    //myLine.Visibility = Visibility.Visible;

        //    //myLine.Stroke = System.Windows.Media.Brushes.Black;
        //    myLine.Stroke = System.Windows.Media.Brushes.Blue;
        //    myLine.SnapsToDevicePixels = true;
        //    myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

        //    myLine.X1 = (int)x1;
        //    myLine.X2 = (int)x2;
        //    myLine.Y1 = (int)y1;
        //    myLine.Y2 = (int)y2;

        //    myLine.StrokeThickness = 1;
        //    drawCanvas.Children.Add(myLine);    
        //}

        //removing
        //private void DrawText(int noOfDevisionsX, int noOfDevisionsY, int intervalValX, int intervalValY)
        //{
        //    double offsetX = (noOfDevisionsY * intervalValY).ToString().Length * 8;
        //    double offsetY = 35;

        //    for (int i = 0; i <= noOfDevisionsX; i++)
        //    {
        //        double x = offsetX + (size.X - offsetX * 2) / noOfDevisionsX * i;
        //        Text(x, size.Y - offsetY, (i * intervalValX).ToString(), Colors.Blue, Double.NaN);
        //    }

        //    int len = (noOfDevisionsY * intervalValY).ToString().Length;
        //    for (int i = 0; i <= noOfDevisionsY; i++)
        //    {
        //        double y = offsetY + (size.Y - offsetY * 2) / noOfDevisionsY * i + 10;
        //        Text(2, size.Y - y, (i * intervalValY).ToString(), Colors.Blue, offsetX - 5);
        //    }

        //    DrawCoordinates(offsetX, 10, offsetX, size.Y - offsetY);
        //    DrawCoordinates(offsetX, size.Y - offsetY, size.X - 10, size.Y - offsetY);
        //}

        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            int noOfDevisionsX = 12;
            int noOfDevisionsY = 10;
            int intervalValX = 1;
            int intervalValY = 100;

            int offsetX = 20;
            int offsetY = 20;

            double x = double.Parse(txbXcoordinate.Text);
            double y = double.Parse(txbYcoordinate.Text);

            // remove
            //int width = (int)((size.X - offsetX * 2) * x / (intervalValX * noOfDevisionsX)) + offsetX;
            //int top = (int)size.Y - (int)((size.Y - offsetY * 2) * y / (intervalValY * noOfDevisionsY)) - offsetY;
            //polyline.Visibility = Visibility.Visible;
            //polyline.SnapsToDevicePixels = true;
            //polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            //lbxPointCollection.Add(new Point(x, y));
            //CalculatePoint(lbxPointCollection);

            // new
            canvasHandler.AddToPolyLine(new Point(x, y));
            
            lbxPoints.Items.Refresh();
            
        }

        //remove
        //private PointCollection CalculatePoint(PointCollection pointCollection)
        //{
        //    PointCollection newPointCollection = new PointCollection();
        //    points.Clear();
        //    int noOfDevisionsX = int.Parse(tbxNumDevX.Text);
        //    int noOfDevisionsY = int.Parse(tbxNumDevY.Text);
        //    int intervalValX = int.Parse(tbxIntervalValX.Text);
        //    int intervalValY = int.Parse(tbxIntervalValY.Text);
        //    int offsetX = 35;
        //    int offsetY = 35;

        //    foreach (var p in pointCollection)
        //    {
        //        Point pp = new Point();
        //        pp.X = (int)((size.X - offsetX * 2) * p.X / (intervalValX * noOfDevisionsX)) + offsetX;
        //        pp.Y = (int)size.Y - (int)((size.Y - offsetY * 2) * p.Y / (intervalValY * noOfDevisionsY)) - offsetY;
        //        points.Add(pp);
        //        Debug.WriteLine(p);

        //        Ellipse ellipse = new Ellipse();
        //        ellipse.Width = ellipse.Height = 5.0;
        //        ellipse.Stroke = Brushes.Red;
        //        ellipse.SetValue(Canvas.LeftProperty, pp.X - 3.0);
        //        ellipse.SetValue(Canvas.TopProperty, pp.Y - 3.0);
        //        drawCanvas.Children.Add(ellipse);
        //    }

        //    return newPointCollection;
        //}

        //removing
        //private void Text(double x, double y, string text, Color color, double width)
        //{
        //    TextBlock textBlock = new TextBlock();
        //    textBlock.TextAlignment = TextAlignment.Right;
        //    textBlock.Width = width;
        //    textBlock.Text = text;
        //    textBlock.Foreground = new SolidColorBrush(color);
        //    Canvas.SetLeft(textBlock, x);
        //    Canvas.SetTop(textBlock, y);
        //    drawCanvas.Children.Add(textBlock);
        //}

        private void btnSettingsOk_Click(object sender, RoutedEventArgs e)
        {
            grbSettings.IsEnabled = false;
            grbAddPoint.IsEnabled = true;

            //drawCanvas.Children.Clear();

            //polyline = new Polyline();
            //polyline.Visibility = Visibility.Visible;

            //polyline.Stroke = System.Windows.Media.Brushes.Red;
            //polyline.SnapsToDevicePixels = true;
            //polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            //polyline.StrokeThickness = 1;
            //drawCanvas.Children.Add(polyline);

            //polyline.Visibility = Visibility.Visible;
            //polyline.Points = points;

            int noOfDevisionsX = int.Parse(tbxNumDevX.Text);
            int noOfDevisionsY = int.Parse(tbxNumDevY.Text);
            int intervalValX = int.Parse(tbxIntervalValX.Text);
            int intervalValY = int.Parse(tbxIntervalValY.Text);

            //new
            canvasHandler.DrawText(noOfDevisionsX, noOfDevisionsY, intervalValX, intervalValY);

            // removed
            //DrawText(noOfDevisionsX, noOfDevisionsY, intervalValX, intervalValY);
            //CalculatePoint(lbxPointCollection);
        }

        public static void WriteXML()
        {
            DiagramData overview = new DiagramData();
            
            //overview = "Serialization Overview";
            //overview.Text = "Bla blra bla...";

            System.Xml.Serialization.XmlSerializer writer =
                new System.Xml.Serialization.XmlSerializer(typeof(DiagramData));

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, overview);
            file.Close();
        }


        public void ReadXML()
        {
            // First write something so that there is something to read ...
            //var b = new DiagramData { title = "Serialization Overview" };
            var b = new DiagramData();
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(DiagramData));
            var wfile = new System.IO.StreamWriter(@"c:\temp\SerializationOverview.xml");
            writer.Serialize(wfile, b);
            wfile.Close();

            // Now we can read the serialized book ...
            System.Xml.Serialization.XmlSerializer reader =
                new System.Xml.Serialization.XmlSerializer(typeof(DiagramData));
            System.IO.StreamReader file = new System.IO.StreamReader(
                @"c:\temp\SerializationOverview.xml");
            DiagramData overview = (DiagramData)reader.Deserialize(file);
            file.Close();

            //Console.WriteLine(overview.title);
        }
    }
}