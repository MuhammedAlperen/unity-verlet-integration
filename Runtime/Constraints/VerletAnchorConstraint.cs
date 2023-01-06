using RopeSystem.Runtime.Particles;
using UnityEngine;

namespace RopeSystem.Runtime.Constraints
{
    public class VerletAnchorConstraint : IVerletParticleConstraint
    {
        public IVerletParticle AnchoredParticle
        {
            get => _anchoredParticle;
            set
            {
                _anchoredParticle = value;
                _anchoredParticle.IsPinned = true;
                _anchoredParticle.Position = AnchorTransform.position;
            }
        }

        public Transform AnchorTransform
        {
            get => _anchorTransform;
            set => _anchorTransform = value;
        }

        private Transform _anchorTransform;
        private IVerletParticle _anchoredParticle;

        public VerletAnchorConstraint(IVerletParticle anchoredParticle, Transform anchorTransform)
        {
            AnchorTransform = anchorTransform;
            AnchoredParticle = anchoredParticle;
        }
        
        public void ResolveConstraint()
        {
            AnchoredParticle.Position = AnchorTransform.position;
        }
    }
}