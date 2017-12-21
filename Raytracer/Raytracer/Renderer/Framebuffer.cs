using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Raytracer.Renderer
{
    public class Framebuffer
    {
        private byte[] _bufferData;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public PixelFormat Format { get; private set; }

        public Framebuffer(int width, int height)
        {
            Width = width;
            Height = height;
            Format = PixelFormats.Bgra32;

            _bufferData = new byte[Width * Height * Format.BitsPerPixel];
        }

        public void Clear(Color clearColor)
        {
            for (var i = 0; i < _bufferData.Length; i += 4)
            {
                _bufferData[i] = clearColor.B;
                _bufferData[i + 1] = clearColor.G;
                _bufferData[i + 2] = clearColor.R;
                _bufferData[i + 3] = clearColor.A;
            }
        }

        public void CopyTo(WriteableBitmap bitmap)
        {
            if (bitmap.Width != Width)
            {
                throw new InvalidOperationException("Framebuffer and WriteableBitmap Width don't match.");
            }

            if (bitmap.Height != Height)
            {
                throw new InvalidOperationException("Framebuffer and WriteableBitmap Height don't match.");
            }

            if (bitmap.Format != Format)
            {
                throw new InvalidOperationException("Framebuffer and WriteableBitmap Format don't match.");
            }

            bitmap.Lock();
            bitmap.WritePixels(new Int32Rect(0, 0, Width, Height), _bufferData, Width * Format.BitsPerPixel, 0);
            bitmap.Unlock();
        }
    }
}
