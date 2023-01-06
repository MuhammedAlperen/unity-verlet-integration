using RopeSystem.Runtime.Particles;
using UnityEngine;

namespace RopeSystem.Runtime.Constraints
{
    public class VerletStick : IVerletParticleConstraint
    {
        public IVerletParticle StartPoint { get; }

        public IVerletParticle EndPoint { get; }

        public float Length { get; set; }

        public float Stiffness { get; set; }

        public VerletStick(IVerletParticle startPoint, IVerletParticle endPoint) : this(startPoint, endPoint, 1f, (startPoint.Position - endPoint.Position).magnitude) { }

        public VerletStick(IVerletParticle startPoint, IVerletParticle endPoint, float stiffness) : this(startPoint, endPoint, stiffness, (startPoint.Position - endPoint.Position).magnitude) { }
        
        public VerletStick(IVerletParticle startPoint, IVerletParticle endPoint, float stiffness, float length)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Stiffness = stiffness;
            Length = length;
        }

        public void ResolveConstraint()
        {
            var delta = EndPoint.Position - StartPoint.Position;
            var distance = delta.magnitude;
            var error = Length - distance;
            var percent = error / distance / 2;
            var correction = delta * (percent * Stiffness);

            if (!StartPoint.IsPinned) StartPoint.Position -= correction;
            if (!EndPoint.IsPinned) EndPoint.Position += correction;
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(StartPoint.Position, EndPoint.Position);
        }
    }
}