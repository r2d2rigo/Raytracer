using System;
using System.Numerics;

namespace Raytracer.Geometry
{
    public struct Ray
    {
        private static readonly float MINIMUM_VECTORS_ANGLE = 0.0001f;

        public Vector3 Position;
        public Vector3 Direction;

        public Ray(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }

        public bool Intersects(Plane plane)
        {
            var vectorsAngle = Vector3.Dot(plane.Normal, Direction);

            if (vectorsAngle >= MINIMUM_VECTORS_ANGLE)
            {
                var planePosition = plane.Normal * plane.D;
                var planeToRay = planePosition - Position;
                var t = Vector3.Dot(planeToRay, plane.Normal) / vectorsAngle;

                return t >= 0;
            }

            return false;
        }
    }
}
