using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

/*
 * Author:          Magnus Sundström
 * Creation Date:   2015-02-01
 * File:            CanvasHandler.cs
 */

namespace DiagramGenerator
{
    /// <summary>
    /// This class handles the canvas by using the classes diagramdata and trace.
    /// </summary>
    class CanvasHandler
    {
        private Canvas canvas;
        private DiagramData diagramData;
        private Polyline polyline;
        private Trace tracer;
        private Axis axis;

        /// <summary>
        /// The constructor of the class. Here all the member variables are initiated.
        /// </summary>
        /// <param name="canvas">The canvas which the plot is drawn.</param>
        /// <param name="diagramData">The diagramdata holds the data of the coordinatesystem and the plot data.</param>
        public CanvasHandler(Canvas canvas, DiagramData diagramData)
        {
            this.canvas = canvas;
            this.diagramData = diagramData;
            this.tracer = new Trace(canvas, diagramData);
            this.axis = new Axis(canvas, diagramData);
            this.polyline = new Polyline();
            PolylineSettings(polyline);
        }

        /// <summary>
        /// The property of the tracer.
        /// </summary>
        public Trace Tracer 
        { 
            get { return this.tracer; } 
        }

        /// <summary>
        /// Initiate the plot by setting the polylines default values.
        /// </summary>
        /// <param name="polyline">The polyline represent the plot.</param>
        public void PolylineSettings(Polyline polyline)
        {
            polyline.Visibility = Visibility.Visible;
            polyline.Stroke = System.Windows.Media.Brushes.Red;
            polyline.SnapsToDevicePixels = true;
            polyline.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            polyline.StrokeThickness = 1;
            canvas.Children.Add(polyline);
            polyline.Visibility = Visibility.Visible;
            polyline.Points = diagramData.GetCanvasPoints();
            AddDots();
        }

        /// <summary>
        /// Sorts the plot based on x-axis.
        /// </summary>
        public void SortXCanvasPoints()
        {
            diagramData.SortXPoints();
            polyline.Points = diagramData.GetCanvasPoints();
        }

        /// <summary>
        /// Sorts the plot based on y-axis.
        /// </summary>
        public void SortYCanvasPoints()
        {
            diagramData.SortYPoints();
            polyline.Points = diagramData.GetCanvasPoints();
        }

        /// <summary>
        /// Add a point to the plot.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        public void AddToPolyLine(Point point)
        {
            diagramData.AddNewPoint(point);
            AddDot(diagramData.LastCanvasPoint);
        }

        /// <summary>
        /// Adds a dot in the end of the polyline.
        /// </summary>
        /// <param name="point">The point to be added.</param>
        public void AddDot(Point point)
        {
            Debug.WriteLine(point);
            Ellipse ellipse = new Ellipse();
            ellipse.Width = ellipse.Height = 5.0;
            ellipse.Stroke = Brushes.Red;
            ellipse.SetValue(Canvas.LeftProperty, point.X - 3.0);
            ellipse.SetValue(Canvas.TopProperty, point.Y - 3.0);
            canvas.Children.Add(ellipse);
        }

        /// <summary>
        /// Method for recreating all the dots in the polyline.
        /// </summary>
        private void AddDots()
        {
            for (int index = 0; index < diagramData.NumberOfPoints; index++)
            {
                AddDot(diagramData.GetCanvasPoint(index));
            }
        }

        /// <summary>
        /// Clears the canvas on all its dots.
        /// </summary>
        public void ClearCanvas()
        {
            canvas.Children.Clear();
            diagramData.ClearPoints();
        }
    }
}