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

namespace DiagramGenerator
{
    class CanvasHandler
    {
        private Canvas canvas;
        private DiagramData diagramData;
        private Polyline polyline;
        private Trace tracer;
        private Axis axis;

        public CanvasHandler(Canvas canvas, DiagramData diagramData)
        {
            this.canvas = canvas;
            this.diagramData = diagramData;
            this.tracer = new Trace(canvas, diagramData);
            this.axis = new Axis(canvas, diagramData);
            this.polyline = new Polyline();
            PolylineSettings(polyline);
        }

        public Trace Tracer 
        { 
            get { return this.tracer; } 
        }

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

        public void SortXCanvasPoints()
        {
            diagramData.SortXPoints();
            polyline.Points = diagramData.GetCanvasPoints();
        }

        public void SortYCanvasPoints()
        {
            diagramData.SortYPoints();
            polyline.Points = diagramData.GetCanvasPoints();
        }

        public void AddToPolyLine(Point point)
        {
            diagramData.AddNewPoint(point);
            AddDot(diagramData.LastCanvasPoint);
        }

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

        private void AddDots()
        {
            for (int index = 0; index < diagramData.NumberOfPoints; index++)
            {
                AddDot(diagramData.GetCanvasPoint(index));
            }
        }

        public void ClearCanvas()
        {
            canvas.Children.Clear();
            diagramData.ClearPoints();
        }
    }
}