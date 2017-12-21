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
                var planePosition = plane.Normal * plane.Distance;
                var planeToRay = planePosition - Position;
                var t = Vector3.Dot(planeToRay, plane.Normal) / vectorsAngle;

                return t >= 0;
            }

            return false;
        }

        public bool Intersects(Sphere sphere)
        {
            var rayToSphere = sphere.Position - Position;
            float rayTangent = Vector3.Dot(rayToSphere, Direction);
            float tangentDistanceSquared = Vector3.Dot(rayToSphere, rayToSphere) - (rayTangent * rayTangent);

            var squaredRadius = sphere.Radius * sphere.Radius;
            if (tangentDistanceSquared > squaredRadius)
            {
                return false;
            }

            float intersectionToTangent = (float)Math.Sqrt(squaredRadius - tangentDistanceSquared);
            var distanceToIntersection1 = rayTangent - intersectionToTangent;
            var distanceToIntersection2 = rayTangent + intersectionToTangent;

            if (distanceToIntersection1 > distanceToIntersection2)
            {
                var temp = distanceToIntersection1;
                distanceToIntersection1 = distanceToIntersection2;
                distanceToIntersection2 = temp;
            }

            if (distanceToIntersection1 < 0)
            {
                distanceToIntersection1 = distanceToIntersection2;

                if (distanceToIntersection1 < 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
