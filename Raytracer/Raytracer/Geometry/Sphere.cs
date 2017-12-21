using System.Numerics;
using System.Windows.Media;

namespace Raytracer.Geometry
{
    public struct Sphere
    {
        public Vector3 Position;
        public float Radius;
        public Color Color;

        public Sphere(Vector3 position, float radius, Color color)
        {
            Position = position;
            Radius = radius;
            Color = color;
        }
    }
}
