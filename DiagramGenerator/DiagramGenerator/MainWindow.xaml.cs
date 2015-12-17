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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RenderTargetBitmap buffer;
        private DrawingVisual drawingVisual = new DrawingVisual();

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            buffer = new RenderTargetBitmap((int)Background.Width, (int)Background.Height, 96, 96, PixelFormats.Pbgra32);
            Background.Source = buffer;
            DrawStuff();
        }

        private void DrawStuff()
        {
            if (buffer == null)
                return;

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.Red), null, new Rect(0, 0, 10, 10));
            }

            buffer.Render(drawingVisual);
        }
    }
}