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

        public void Intersects(Plane plane, out IntersectionResult result)
        {
            var vectorsAngle = Vector3.Dot(plane.Normal, Direction);

            result.IsHit = false;
            result.Length = 0;

            if (vectorsAngle >= MINIMUM_VECTORS_ANGLE)
            {
                var planePosition = plane.Normal * plane.Distance;
                var planeToRay = planePosition - Position;
                var length = Vector3.Dot(planeToRay, plane.Normal) / vectorsAngle;

                if (length  >= 0)
                {
                    result.IsHit = true;
                    result.Length = length;
                }
            }
        }

        public void Intersects(Sphere sphere, out IntersectionResult result)
        {
            var rayToSphere = sphere.Position - Position;
            float rayTangent = Vector3.Dot(rayToSphere, Direction);
            float tangentDistanceSquared = Vector3.Dot(rayToSphere, rayToSphere) - (rayTangent * rayTangent);

            result.IsHit = false;
            result.Length = 0;

            var squaredRadius = sphere.Radius * sphere.Radius;
            if (tangentDistanceSquared > squaredRadius)
            {
                return;
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
                    return;
                }
            }

            result.IsHit = true;
            result.Length = distanceToIntersection1;
        }
    }
}
