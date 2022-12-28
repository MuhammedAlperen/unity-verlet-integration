using System;
using UnityEngine;

namespace RopeSystem.Runtime
{
    [Serializable]
    public class RopeStick
    {
        [SerializeReference] private RopePoint m_startPoint;
        [SerializeReference] private RopePoint m_endPoint;
        [SerializeField] private float m_length;
        [SerializeField] private float m_stiffness;

        public RopeStick(RopePoint startPoint, RopePoint endPoint) : this(startPoint, endPoint, 1f, (startPoint.Position - endPoint.Position).magnitude) { }
        
        public RopeStick(RopePoint startPoint, RopePoint endPoint, float stiffness) : this(startPoint, endPoint, stiffness, (startPoint.Position - endPoint.Position).magnitude) { }
        
        public RopeStick(RopePoint startPoint, RopePoint endPoint, float stiffness, float length)
        {
            m_startPoint = startPoint;
            m_endPoint = endPoint;
            m_stiffness = stiffness;
            m_length = length;
        }

        public RopePoint StartPoint => m_startPoint;
        public RopePoint EndPoint => m_endPoint;
        public float Length => m_length;
        public float Stiffness => m_stiffness;

        public void UpdateLength()
        {
            m_length = (StartPoint.Position - EndPoint.Position).magnitude;
        }
    }
}