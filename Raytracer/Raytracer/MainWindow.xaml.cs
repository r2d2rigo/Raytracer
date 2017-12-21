using Raytracer.Graphics;
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
        private Renderer _renderer;
        private WriteableBitmap _framebufferTarget;

        public MainWindow()
        {
            InitializeComponent();

            _framebuffer = new Framebuffer(640, 480);
            _framebufferTarget = new WriteableBitmap(_framebuffer.Width, _framebuffer.Height, 96.0, 96.0, _framebuffer.Format, null);
            _renderer = new Renderer();

            _target.Source = _framebufferTarget;

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RenderScene();
        }

        private void RenderScene()
        {
            _renderer.Render(_framebuffer);
            _framebuffer.CopyTo(_framebufferTarget);
        }
    }
}
