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

        public CanvasHandler(Canvas canvas, DiagramData diagramData)
        {
            this.canvas = canvas;
            this.diagramData = diagramData;
            CreatePolyLine();
            
            canvas.Children.Add(xLine);
            canvas.Children.Add(yLine);

            xLine.X1 = 0;
            xLine.X2 = canvas.Width;
            xLine.Stroke = System.Windows.Media.Brushes.Blue;
            xLine.SnapsToDevicePixels = true;
            xLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            xLine.StrokeThickness = 1;
            xLine.StrokeDashArray = new DoubleCollection() { 4, 2 };

            yLine.Y1 = 0;
            yLine.Y2 = canvas.Height;
            yLine.Stroke = System.Windows.Media.Brushes.Blue;
            yLine.SnapsToDevicePixels = true;
            yLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            yLine.StrokeThickness = 1;
            //yLine.Visibility = Visibility.Hidden;
            yLine.StrokeDashArray = new DoubleCollection() { 4, 2 };
        }

        private void CreatePolyLine()
        {
            polyline = new Polyline();
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

        Line xLine = new Line();
        Line yLine = new Line();

        public void Cross(Point point)
        {
           
            xLine.Y1 = xLine.Y2 = point.Y;
            yLine.X1 = yLine.X2 = point.X;
            

        }

        public void SortCanvasPoints()
        {
            diagramData.SortPoints();
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

        public void DrawText(int noOfDevisionsX, int noOfDevisionsY, int intervalValX, int intervalValY)
        {
            diagramData.InterValValueX = intervalValX;
            diagramData.InterValValueY = intervalValY;
            diagramData.NumberOfDevisionsX = noOfDevisionsX;
            diagramData.NumberOfDevisionsY = noOfDevisionsY;
            
            double offsetX = (noOfDevisionsY * intervalValY).ToString().Length * 8;
            double offsetY = 35;

            for (int i = 0; i <= noOfDevisionsX; i++)
            {
                double x = offsetX + (diagramData.CanvasWidth - offsetX * 2) / noOfDevisionsX * i;
                Text(x, diagramData.CanvasHeight - offsetY, (i * intervalValX).ToString(), Colors.Blue, Double.NaN);
            }

            int len = (noOfDevisionsY * intervalValY).ToString().Length;
            for (int i = 0; i <= noOfDevisionsY; i++)
            {
                double y = offsetY + (diagramData.CanvasHeight - offsetY * 2) / noOfDevisionsY * i + 10;
                Text(2, diagramData.CanvasHeight - y, (i * intervalValY).ToString(), Colors.Blue, offsetX - 5);
            }

            DrawCoordinates(offsetX, 10, offsetX, diagramData.CanvasHeight - offsetY);
            DrawCoordinates(offsetX, diagramData.CanvasHeight - offsetY, diagramData.CanvasWidth - 10, diagramData.CanvasHeight - offsetY);
        }

        private void DrawCoordinates(double x1, double y1, double x2, double y2)
        {
            Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.Blue;
            myLine.SnapsToDevicePixels = true;
            myLine.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);

            myLine.X1 = (int)x1;
            myLine.X2 = (int)x2;
            myLine.Y1 = (int)y1;
            myLine.Y2 = (int)y2;

            myLine.StrokeThickness = 1;
            canvas.Children.Add(myLine);
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
            canvas.Children.Add(textBlock);
        }

        public void ClearCanvas()
        {
            diagramData.ClearPoints();
            canvas.Children.Clear();
            CreatePolyLine();
        }
    }
}
