using System;
using RopeSystem.Runtime.Particles;
using UnityEngine;

namespace RopeSystem.Runtime.Constraints
{
    [Serializable]
    public class VerletStick : IVerletParticleConstraint
    {
        [SerializeReference] private VerletParticle m_startPoint;
        [SerializeReference] private VerletParticle m_endPoint;
        [SerializeField] private float m_length;
        [SerializeField] private float m_stiffness;

        public VerletParticle StartPoint => m_startPoint;
        public VerletParticle EndPoint => m_endPoint;
        public float Length => m_length;
        public float Stiffness => m_stiffness;

        public VerletStick(VerletParticle startPoint, VerletParticle endPoint) : this(startPoint, endPoint, 1f, (startPoint.Position - endPoint.Position).magnitude) { }

        public VerletStick(VerletParticle startPoint, VerletParticle endPoint, float stiffness) : this(startPoint, endPoint, stiffness, (startPoint.Position - endPoint.Position).magnitude) { }
        
        public VerletStick(VerletParticle startPoint, VerletParticle endPoint, float stiffness, float length)
        {
            m_startPoint = startPoint;
            m_endPoint = endPoint;
            m_stiffness = stiffness;
            m_length = length;
        }

        public void UpdateLength()
        {
            m_length = (StartPoint.Position - EndPoint.Position).magnitude;
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
    }
}