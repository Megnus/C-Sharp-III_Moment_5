using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DiagramGenerator
{
    class Trace
    {
        private Line xLine;
        private Line yLine;
        private Canvas canvas;
        private DiagramData diagramData;
        TextBlock crossText = new TextBlock();

        public void CreateCross(Canvas canvas, DiagramData diagramData)
        {
            //xLine = new Line();
            //yLine = new Line();
            this.canvas = canvas;
            this.diagramData = diagramData;

            CreateTraceLine(xLine);
            CreateTraceLine(yLine);
        }

        private void CreateTraceLine(Line line)
        {
            line = new Line();
            line.X1 = 0;
            line.X2 = canvas.Width;
            line.Stroke = System.Windows.Media.Brushes.DarkGreen;
            line.SnapsToDevicePixels = true;
            line.SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            line.StrokeThickness = 1;
            line.Visibility = Visibility.Hidden;
            line.StrokeDashArray = new DoubleCollection() { 4, 2 };
            canvas.Children.Add(line);
        }

        public void SetCrossVisiblity(Visibility vis)
        {
            xLine.Visibility = vis;
            yLine.Visibility = vis;
            crossText.Visibility = vis;
        }

        public void Cross(Point point)
        {
            xLine.Y1 = xLine.Y2 = point.Y;
            yLine.X1 = yLine.X2 = point.X;

            xLine.Visibility = Visibility.Visible;
            yLine.Visibility = Visibility.Visible;

            canvas.Children.Remove(crossText);
            crossText.TextAlignment = TextAlignment.Right;
            //crossText.Width = width;
            Point p = diagramData.Reverse(point);
            crossText.Text = string.Format("({0}, {1})", p.X.ToString("0.00"), p.Y.ToString("0.00"));
            crossText.Foreground = new SolidColorBrush(Colors.DarkGreen);
            Canvas.SetLeft(crossText, point.X + 5);
            Canvas.SetTop(crossText, point.Y + 5);
            canvas.Children.Add(crossText);
        }

    }
}
