﻿using Raytracer.Geometry;
using Raytracer.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Media;
using Plane = Raytracer.Geometry.Plane;

namespace Raytracer
{
    public class Renderer
    {
        public void Render(Framebuffer target)
        {
            target.Clear(Colors.Black);

            var targetAspectRatio = target.Width / (float)target.Height;

            var camera = new Camera(Vector3.Zero, new Vector3(0.0f, 0.0f, -3.0f), 60.0f);
            var fovTangent = (float)Math.Tan(camera.Fov * Math.PI / 180.0);

            var groundPlane = new Plane(Vector3.UnitY, 0f, Colors.Gray);

            var spheres = new List<Sphere>();
            spheres.Add(new Sphere(new Vector3(-0.75f, 0.25f, -1.0f), 0.25f, Colors.CornflowerBlue));
            spheres.Add(new Sphere(new Vector3(1.0f, -0.25f, -1.5f), 0.5f, Colors.BlueViolet));
            spheres.Add(new Sphere(new Vector3(-0.75f, -0.4f, -2f), 0.75f, Colors.LightPink));
            spheres.Add(new Sphere(new Vector3(1.0f, 0.25f, -2.25f), 0.5f, Colors.LimeGreen));
            spheres.Add(new Sphere(new Vector3(0.0f, 0.0f, -3.0f), 1.0f, Colors.White));

            for (int y = 0; y < target.Height; y++)
            {
                for (int x = 0; x < target.Width; x++)
                {
                    var pixelDeviceCoordinates = new Vector2(
                        (x + 0.5f) / target.Width, 
                        (y + 0.5f) / target.Height);
                    var pixelScreenCoordinates = new Vector2(
                        (2.0f * pixelDeviceCoordinates.X) - 1.0f,
                        (2.0f * pixelDeviceCoordinates.Y) - 1.0f);
                    var pixelCameraCoordinates = new Vector3(
                        pixelScreenCoordinates.X * targetAspectRatio * fovTangent,
                        pixelScreenCoordinates.Y * fovTangent,
                        -1.0f);

                    var pixelRayDirection = Vector3.Normalize(pixelCameraCoordinates - camera.Position);
                    pixelRayDirection = Vector3.Transform(pixelRayDirection, camera.CameraToWorld);

                    var hitDistance = float.MaxValue;

                    var ray = new Ray(camera.Position, Vector3.Normalize(pixelRayDirection));
                    var hitResult = new IntersectionResult();

                    ray.Intersects(groundPlane, out hitResult);

                    if (hitResult.IsHit && hitResult.Length < hitDistance)
                    {
                        target.SetPixel(x, y, groundPlane.Color);
                    }

                    foreach (var sphere in spheres)
                    {
                        ray.Intersects(sphere, out hitResult);

                        if (hitResult.IsHit && hitResult.Length < hitDistance)
                        {
                            target.SetPixel(x, y, sphere.Color);
                            hitDistance = hitResult.Length;
                        }
                    }
                }
            }
        }
    }
}
