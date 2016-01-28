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
using UtilitiesLib;

namespace DiagramGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiagramData diagramData;
        private CanvasHandler canvasHandler;

        public MainWindow()
        {
            InitializeComponent();
            diagramData = new DiagramData();
            diagramData.CanvasWidth = drawCanvas.Width;
            diagramData.CanvasHeight = drawCanvas.Height;
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
            canvasHandler = new CanvasHandler(drawCanvas, diagramData);
        }

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
            double x = 0, y = 0;

            if (!ValidateTextField(txbXcoordinate, out x) || !ValidateTextField(txbYcoordinate, out y))
            {
                return;
            };

            canvasHandler.AddToPolyLine(new Point(x, y));
            lbxPoints.Items.Refresh();
        }

        private bool ValidateTextField(TextBox textBox, out double value)
        {
            value = InputHandeling.PareseDouble(textBox.Text);

            if (!InputHandeling.DoubleIsParsable(textBox.Text))
            {
                textBox.Focus();
                textBox.SelectAll();
                return false;
            }

            return true;
        }

        private bool ValidateTextField(TextBox textBox, out int value)
        {
            double temp;
            bool result = ValidateTextField(textBox, out temp);
            value = (int)temp;
            return result;
        }

        private void btnSettingsOk_Click(object sender, RoutedEventArgs e)
        {
            int numberOfDevisionsX = 0, numberOfDevisionsY = 0;
            double interValValueX = 0, interValValueY = 0;

            if (!ValidateTextField(tbxNumDevX, out numberOfDevisionsX)
                || !ValidateTextField(tbxNumDevY, out numberOfDevisionsY)
                || !ValidateTextField(tbxIntervalValX, out interValValueX)
                || !ValidateTextField(tbxIntervalValY, out interValValueY))
            {
                return;
            };

            diagramData.InterValValueY = interValValueY;
            diagramData.NumberOfDevisionsX = numberOfDevisionsX;
            diagramData.NumberOfDevisionsY = numberOfDevisionsY;
            diagramData.InterValValueX = interValValueX;

            grbSettings.IsEnabled = false;
            grbAddPoint.IsEnabled = true;

            canvasHandler.ClearCanvas();
            canvasHandler = new CanvasHandler(drawCanvas, diagramData);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            diagramData = XmlFileHandler.ReadXML();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
            lbxPoints.Items.Refresh();
            tbxNumDevX.Text = diagramData.NumberOfDevisionsX.ToString();
            tbxNumDevY.Text = diagramData.NumberOfDevisionsY.ToString();
            tbxIntervalValX.Text = diagramData.InterValValueX.ToString();
            tbxIntervalValY.Text = diagramData.InterValValueY.ToString();
            grbSettings.IsEnabled = false;
            grbAddPoint.IsEnabled = true;

            canvasHandler.ClearCanvas();
            canvasHandler = new CanvasHandler(drawCanvas, diagramData);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            XmlFileHandler.WriteXML(diagramData);
        }

        private void SortX_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.SortXCanvasPoints();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
        }

        private void SortY_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.SortYCanvasPoints();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
        }

        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(drawCanvas);
            canvasHandler.Tracer.SetTrace(point);
        }

        private void drawCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            canvasHandler.Tracer.SetCrossVisiblity(Visibility.Hidden);
        }

        private void drawCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            canvasHandler.Tracer.SetCrossVisiblity(Visibility.Visible);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}