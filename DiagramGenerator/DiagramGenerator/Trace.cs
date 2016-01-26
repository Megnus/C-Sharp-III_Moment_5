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
        private TextBlock tracerText = new TextBlock();

        public Trace(Canvas canvas, DiagramData diagramData)
        {
            this.canvas = canvas;
            this.diagramData = diagramData;

            xLine = CreateTraceLine();
            yLine = CreateTraceLine();
        }

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

        public void SetCrossVisiblity(Visibility vis)
        {
            xLine.Visibility = vis;
            yLine.Visibility = vis;
            tracerText.Visibility = vis;
        }

        public void SetTrace(Point point)
        {
            xLine.Y1 = xLine.Y2 = point.Y;
            yLine.X1 = yLine.X2 = point.X;
            xLine.Visibility = yLine.Visibility = Visibility.Visible;
            SetText(point);
        }

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
