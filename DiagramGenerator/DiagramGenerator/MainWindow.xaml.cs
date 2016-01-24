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
 *      j) Importera dll för att testa int osv.
 *      k) Skicka in lösningen.
*/

namespace DiagramGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // stores the collection of points for the mutisided shapes

        private DiagramData diagramData;
        private CanvasHandler canvasHandler;

        // initialize the points of the shapes
        public MainWindow()
        {
            InitializeComponent();
            diagramData = new DiagramData();
            diagramData.CanvasWidth = drawCanvas.Width;
            diagramData.CanvasHeight = drawCanvas.Height;
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
            canvasHandler = new CanvasHandler(drawCanvas, diagramData);
            // create cross
            canvasHandler.CreateCross();
        }

        // when the clear Button is clicked
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.ClearCanvas();
            lbxPoints.Items.Refresh();
            btnSettingsOk_Click(sender, e);
            grbSettings.IsEnabled = true;
            grbAddPoint.IsEnabled = false;
            txbXcoordinate.Text = string.Empty;
            txbYcoordinate.Text = string.Empty;
        }

        private void btnAddPoint_Click(object sender, RoutedEventArgs e)
        {
            double x = double.Parse(txbXcoordinate.Text);
            double y = double.Parse(txbYcoordinate.Text);
            canvasHandler.AddToPolyLine(new Point(x, y));
            lbxPoints.Items.Refresh();
        }

        private void btnSettingsOk_Click(object sender, RoutedEventArgs e)
        {
            grbSettings.IsEnabled = false;
            grbAddPoint.IsEnabled = true;
            canvasHandler.ClearCanvas();
            
            int noOfDevisionsX = int.Parse(tbxNumDevX.Text);
            int noOfDevisionsY = int.Parse(tbxNumDevY.Text);
            int intervalValX = int.Parse(tbxIntervalValX.Text);
            int intervalValY = int.Parse(tbxIntervalValY.Text);
            canvasHandler.DrawText(noOfDevisionsX, noOfDevisionsY, intervalValX, intervalValY);
        }

        private void MenuItem_Open_Click(object sender, RoutedEventArgs e)
        {
            diagramData = XmlFileHandler.ReadXML();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
            lbxPoints.Items.Refresh();
            tbxNumDevX.Text = diagramData.NumberOfDevisionsX.ToString();
            tbxNumDevY.Text = diagramData.NumberOfDevisionsY.ToString();
            tbxIntervalValX.Text = diagramData.InterValValueX.ToString();
            tbxIntervalValY.Text = diagramData.InterValValueY.ToString();
            btnSettingsOk_Click(sender, e);
            grbSettings.IsEnabled = false;
            grbAddPoint.IsEnabled = true;

            canvasHandler = new CanvasHandler(drawCanvas, diagramData);
            canvasHandler.CreateCross();
        }

        private void MenuItem_Save_Click(object sender, RoutedEventArgs e)
        {
            XmlFileHandler.WriteXML(diagramData);   
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.SortCanvasPoints();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            var p = e.GetPosition(drawCanvas);
           canvasHandler.Cross(p);
        }

        private void drawCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            canvasHandler.SetCrossVisiblity(Visibility.Hidden);
        }

        private void drawCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            canvasHandler.SetCrossVisiblity(Visibility.Visible);
        }


    }
}