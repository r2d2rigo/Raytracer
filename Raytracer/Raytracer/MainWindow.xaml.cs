using Raytracer.Renderer;
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
        private Framebuffer _framebuffer;
        private WriteableBitmap _framebufferTarget;

        public MainWindow()
        {
            InitializeComponent();

            _framebuffer = new Framebuffer(640, 480);
            _framebuffer.Clear(Colors.CornflowerBlue);
            _framebufferTarget = new WriteableBitmap(_framebuffer.Width, _framebuffer.Height, 96.0, 96.0, _framebuffer.Format, null);
            _framebuffer.CopyTo(_framebufferTarget);

            _target.Source = _framebufferTarget;
        }
    }
}
