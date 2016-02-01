using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using UtilitiesLib;

/*
 * Author:          Magnus Sundström
 * Creation Date:   2015-02-01
 * File:            MainWindow.xaml.cs
 */

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
            diagramData.CanvasWidth = canvas.Width;
            diagramData.CanvasHeight = canvas.Height;
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
            canvasHandler = new CanvasHandler(canvas, diagramData);
        }

        /// <summary>
        /// Event that clears the plots.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
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

        /// <summary>
        /// Event to add a point to the plot.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
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

        /// <summary>
        /// Method to validate if the text in a textbox could be parsed to a double.
        /// </summary>
        /// <param name="textBox">The textbox in focus.</param>
        /// <param name="value">The value in the textfiled.</param>
        /// <returns>If the textbox contains a double then return true and false if not.</returns>
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

        /// <summary>
        /// Method to validate if the text in a textbox could be parsed to a integer.
        /// </summary>
        /// <param name="textBox">The textbox in focus.</param>
        /// <param name="value">The value in the textfiled.</param>
        /// <returns>If the textbox contains a integer then return true and false if not.</returns>
        private bool ValidateTextField(TextBox textBox, out int value)
        {
            double temp;
            bool result = ValidateTextField(textBox, out temp);
            value = (int)temp;
            return result;
        }

        /// <summary>
        /// Action fired by a button click and indicates that the coordinates shall be created.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
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
            canvasHandler = new CanvasHandler(canvas, diagramData);
        }

        /// <summary>
        /// Opens a xml-file from a input dialog. The xml-file contains the data for the coordinatesystem
        /// and the plot.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
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
            canvasHandler = new CanvasHandler(canvas, diagramData);
        }

        /// <summary>
        /// Saves the plot and the coordiantesystem to a xml-ifle.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            XmlFileHandler.WriteXML(diagramData);
        }

        /// <summary>
        /// Sorts the plot in respect of the values of the x-axis.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortX_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.SortXCanvasPoints();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
        }

        /// <summary>
        /// Sorts the plot in respect of the values of the y-axis.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
        private void SortY_Click(object sender, RoutedEventArgs e)
        {
            canvasHandler.SortYCanvasPoints();
            lbxPoints.ItemsSource = diagramData.GetCoordinatePoints();
        }

        /// <summary>
        /// Sets the tracer lines by the mouse move input.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
        private void drawCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(canvas);
            canvasHandler.Tracer.SetTrace(point);
        }

        /// <summary>
        /// Disables the tracer when the mouse cursor enters the canvas.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
        private void drawCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            canvasHandler.Tracer.SetCrossVisiblity(Visibility.Hidden);
        }

        /// <summary>
        /// Enables the tracer when the mouse cursor enters the canvas.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
        private void drawCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            canvasHandler.Tracer.SetCrossVisiblity(Visibility.Visible);
        }

        /// <summary>
        /// Exits the application when the "Exit" is chosen from the menu.
        /// </summary>
        /// <param name="sender">The component which delivers the action.</param>
        /// <param name="e">The arguments from the sender.</param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}