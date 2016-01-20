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
    class ImageDrawing : Window
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
            DrawImage();
        }

        private void DrawImage()
        {
            if (buffer == null)
            {
                return;
            }

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.Red), null, new Rect(0, 0, 10, 10));
                buffer.Render(drawingVisual);
            }
        }
    }
}
