using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Raytracer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WriteableBitmap _framebuffer;

        public MainWindow()
        {
            InitializeComponent();

            _framebuffer = new WriteableBitmap(640, 480, 96.0, 96.0, PixelFormats.Bgra32, null);
            _framebuffer.Clear(Colors.Black);
            _target.Source = _framebuffer;
        }
    }
}
