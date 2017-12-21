using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Raytracer.Graphics
{
    public class Camera
    {
        private Vector3 _position;
        private Vector3 _target;

        public Vector3 Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    RecalculateMatrix();
                }
            }
        }

        public Vector3 Target
        {
            get { return _target; }
            set
            {
                if (_target != value)
                {
                    _target = value;
                    RecalculateMatrix();
                }
            }
        }

        public Matrix4x4 CameraToWorld { get; private set; }

        public Camera(Vector3 position, Vector3 target)
        {
            Position = position;
            Target = target;

            RecalculateMatrix();
        }

        private void RecalculateMatrix()
        {
            var lookAtMatrix = Matrix4x4.CreateLookAt(Position, Target, Vector3.UnitY);
            Matrix4x4.Invert(lookAtMatrix, out lookAtMatrix);
            CameraToWorld = lookAtMatrix;
        }
    }
}
