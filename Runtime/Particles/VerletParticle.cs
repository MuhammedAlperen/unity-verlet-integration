using UnityEngine;

namespace RopeSystem.Runtime.Particles
{
    [System.Serializable]
    public class VerletParticle : IVerletParticle
    {
        [SerializeField] private Vector3 m_position;
        [SerializeField] private Vector3 m_previousPosition;

        [SerializeField] private bool m_isPinned;

        public Vector3 Position
        {
            get => m_position;
            set => m_position = value;
        }

        public Vector3 PreviousPosition
        {
            get => m_previousPosition;
            set => m_previousPosition = value;
        }

        public bool IsPinned
        {
            get => m_isPinned;
            set => m_isPinned = value;
        }

        public VerletParticle(Vector3 position) : this(position, false) { }

        public VerletParticle(Vector3 position, bool isPinned)
        {
            Position = position;
            PreviousPosition = position;
            IsPinned = isPinned;
        }
    }
}