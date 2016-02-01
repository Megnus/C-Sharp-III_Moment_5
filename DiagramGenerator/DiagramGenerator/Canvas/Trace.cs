using System;
using System.Collections.Generic;
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
 * File:            Trace.cs
 */

namespace DiagramGenerator
{
    /// <summary>
    /// This class handles the tracer for the canvas.
    /// </summary>
    class Trace
    {
        private Line xLine;
        private Line yLine;
        private Canvas canvas;
        private DiagramData diagramData;
        private TextBlock tracerText = new TextBlock();

        /// <summary>
        /// The constructor for the class. Here the member variables are initiated.
        /// </summary>
        /// <param name="canvas">The canvas which the plot is drawn.</param>
        /// <param name="diagramData">The diagramdata holds the data of the coordinatesystem and the plot data.</param>
        public Trace(Canvas canvas, DiagramData diagramData)
        {
            this.canvas = canvas;
            this.diagramData = diagramData;
            xLine = CreateTraceLine();
            yLine = CreateTraceLine();
        }

        /// <summary>
        /// Initiates and creates the tracer lines.
        /// </summary>
        /// <returns></returns>
        private Line CreateTraceLine()
        {
            Line line = new Line();
            line.X1 = 0;
            line.Y1 = 0;
            line.X2 = canvas.Width;
            line.Y2 = canvas.Height;

            line.Stroke = System.Windows.Media.Brushes.DarkGreen;
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            line.StrokeThickness = 1;
            line.Visibility = Visibility.Hidden;
            line.StrokeDashArray = new DoubleCollection() { 4, 2 };
            canvas.Children.Add(line);
            return line;
        }

        /// <summary>
        /// Sets the visibility of the tracer lines.
        /// </summary>
        /// <param name="visibility">The visibility of the lines. Are used with "hidden" or visible.</param>
        public void SetCrossVisiblity(Visibility visibility)
        {
            xLine.Visibility = visibility;
            yLine.Visibility = visibility;
            tracerText.Visibility = visibility;
        }

        /// <summary>
        /// Sets the the coordinates for the tracer lines.
        /// </summary>
        /// <param name="point">The point of the origo of the lines.</param>
        public void SetTrace(Point point)
        {
            xLine.Y1 = xLine.Y2 = point.Y;
            yLine.X1 = yLine.X2 = point.X;
            xLine.Visibility = yLine.Visibility = Visibility.Visible;
            SetText(point);
        }

        /// <summary>
        /// This method draws the text to the canvas and shows the coordinate value in text form.zs
        /// </summary>
        /// <param name="point">The point to be drawn as text.</param>
        private void SetText(Point point)
        {
            canvas.Children.Remove(tracerText);
            tracerText.TextAlignment = TextAlignment.Right;
            Point p = diagramData.Reverse(point);
            tracerText.Text = string.Format("({0}, {1})", p.X.ToString("0.00"), p.Y.ToString("0.00"));
            tracerText.Foreground = new SolidColorBrush(Colors.DarkGreen);
            Canvas.SetLeft(tracerText, point.X + 5);
            Canvas.SetTop(tracerText, point.Y + 5);
            canvas.Children.Add(tracerText);
        }

    }
}