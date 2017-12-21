using Raytracer.Geometry;
using Raytracer.Graphics;
using System;
using System.Numerics;
using System.Windows.Media;

namespace Raytracer
{
    public class Renderer
    {
        public void Render(Framebuffer target)
        {
            target.Clear(Colors.Black);

            var targetAspectRatio = target.Width / (float)target.Height;
            var fovTangent = (float)Math.Tan(Math.PI / 4.0);

            var groundPlane = new Plane(-Vector3.UnitY, 0);

            for (int y = 0; y < target.Height; y++)
            {
                for (int x = 0; x < target.Width; x++)
                {
                    var pixelDeviceCoordinates = new Vector2(
                        (x + 0.5f) / target.Width, 
                        (y + 0.5f) / target.Height);
                    var pixelScreenCoordinates = new Vector2(
                        (2.0f * pixelDeviceCoordinates.X) - 1.0f,  
                        1.0f - (2.0f * pixelDeviceCoordinates.Y));
                    var pixelCameraCoordinates = new Vector3(
                        pixelScreenCoordinates.X * targetAspectRatio * fovTangent,
                        pixelScreenCoordinates.Y * fovTangent,
                        -1.0f);

                    var cameraPosition = Vector3.Zero;
                    var cameraToWorld = Matrix4x4.CreateLookAt(cameraPosition, -Vector3.UnitZ , Vector3.UnitY);
                    Matrix4x4.Invert(cameraToWorld, out cameraToWorld);
                    var pixelRayDirection = Vector3.Normalize(pixelCameraCoordinates - Vector3.Zero);
                    pixelRayDirection = Vector3.Transform(pixelRayDirection, cameraToWorld);

                    var ray = new Ray(cameraPosition, Vector3.Normalize(pixelRayDirection));

                    if (ray.Intersects(groundPlane))
                    {
                        target.SetPixel(x, y, Colors.Gray);
                    }
                }
            }
        }
    }
}
