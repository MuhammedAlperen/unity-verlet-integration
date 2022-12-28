using System;
using UnityEngine;

namespace RopeSystem.Runtime
{
    [Serializable]
    public class RopePoint
    {
        [SerializeField] private Vector3 m_position;
        [SerializeField] private Vector3 m_previousPosition;

        [SerializeField] private bool m_isPinned;

        public Vector3 Position
        {
            get => m_position;
            internal set => m_position = value;
        }
        
        public Vector3 PreviousPosition
        {
            get => m_previousPosition;
            internal set => m_previousPosition = value;
        }
        
        public bool IsPinned
        {
            get => m_isPinned;
            internal set => m_isPinned = value;
        }
    }
}