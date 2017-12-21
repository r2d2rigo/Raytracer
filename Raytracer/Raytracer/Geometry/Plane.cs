using System.Numerics;
using System.Windows.Media;

namespace Raytracer.Geometry
{
    public struct Plane
    {
        public Vector3 Normal;
        public float Distance;
        public Color Color;

        public Plane(Vector3 normal, float distance, Color color)
        {
            Normal = normal;
            Distance = distance;
            Color = color;
        }
    }
}
