using Raytracer.Graphics;
using System.Windows.Media;

namespace Raytracer
{
    public class Renderer
    {
        public void Render(Framebuffer target)
        {
            target.Clear(Colors.Black);

            for (int y = 0; y < target.Height; y++)
            {
                for (int x = 0; x < target.Width; x++)
                {
                    if (y > target.Height / 2)
                    {
                        target.SetPixel(x, y, Colors.Gray);
                    }
                }
            }
        }
    }
}
