using UnityEngine;

namespace RopeSystem.Runtime.Particles
{
    public interface IVerletParticle
    {
        public Vector3 Position { get; set; }
        public Vector3 PreviousPosition { get; set; }
        public bool IsPinned  { get; set; }
    }
}