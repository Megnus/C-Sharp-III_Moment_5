using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagramGenerator
{
    class Obsolete : Window
    {
        private RenderTargetBitmap buffer;
        private DrawingVisual drawingVisual = new DrawingVisual();

        //<Image Name="Background" HorizontalAlignment="Left" VerticalAlignment="Top" Width="250" Height="250"/>

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            Image background = new Image();
            buffer = new RenderTargetBitmap((int)background.Width, (int)background.Height, 96, 96, PixelFormats.Pbgra32);
            background.Source = buffer;
            DrawStuff();
        }

        private void DrawStuff()
        {
            if (buffer == null)
                return;

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.Red), null, new Rect(0, 0, 10, 10));
                //}

                buffer.Render(drawingVisual);
            }
        }

        /*
        // adds a new point when the user clicks on the canvas
        private void drawCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //add point to collection
            points.Add(e.GetPosition(drawCanvas));
        }
     
        // when the user selects the Polyline
        private void lineRadio_Checked(object sender, RoutedEventArgs e)
        {
            // Polyline is visible, the other two are not
            polyline.Visibility = Visibility.Visible;
            polygon.Visibility = Visibility.Collapsed;
            filledPolygon.Visibility = Visibility.Collapsed;
        }

        // when the user selects the Polygon
        private void polygonRadio_Checked(object sender, RoutedEventArgs e)
        {
            // Polygon is visible, the other two are not
            polyline.Visibility = Visibility.Collapsed;
            polygon.Visibility = Visibility.Visible;
            filledPolygon.Visibility = Visibility.Collapsed;
        }

        // when the user selects the filled Polygon
        private void filledPolygonRadio_Checked(object sender, RoutedEventArgs e)
        {
            // filled Polygon is visible, the other two are not
            polygon.Visibility = Visibility.Collapsed;
            polygon.Visibility = Visibility.Collapsed;
            filledPolygon.Visibility = Visibility.Visible;
        }*/
    }
}
