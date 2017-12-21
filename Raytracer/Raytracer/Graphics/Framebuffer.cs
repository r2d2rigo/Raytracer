using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Raytracer.Graphics
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

        public void SetPixel(int x, int y, Color color)
        {
            if (x < 0 || x >= Width)
            {
                throw new InvalidOperationException("Pixel x value out of range.");
            }

            if (y < 0 || y >= Height)
            {
                throw new InvalidOperationException("Pixel y value out of range.");
            }

            var bufferOffset = (x + (y * Width)) * (Format.BitsPerPixel / 8);
            _bufferData[bufferOffset] = color.B;
            _bufferData[bufferOffset + 1] = color.G;
            _bufferData[bufferOffset + 2] = color.R;
            _bufferData[bufferOffset + 3] = color.A;
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
            bitmap.WritePixels(new Int32Rect(0, 0, Width, Height), _bufferData, Width * (Format.BitsPerPixel / 8), 0);
            bitmap.Unlock();
        }
    }
}
