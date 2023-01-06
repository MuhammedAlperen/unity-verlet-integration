using System.Collections.Generic;
using RopeSystem.Runtime.Constraints;
using RopeSystem.Runtime.Particles;
using UnityEngine;

namespace RopeSystem.Runtime
{
    public class VerletSimulation
    {
        private readonly int _stickResolveIterations;
        private readonly IGravityProvider _gravityProvider;

        private readonly List<IVerletParticle> _particles;
        private readonly List<IVerletParticleConstraint> _constraints;
        
        public IReadOnlyList<IVerletParticle> ParticleList => _particles;
        public IReadOnlyList<IVerletParticleConstraint> ConstraintList => _constraints;

        public VerletSimulation(int stickResolveIteration, IGravityProvider gravityProvider)
        {
            _stickResolveIterations = stickResolveIteration;
            _gravityProvider = gravityProvider;

            _particles = new List<IVerletParticle>();
            _constraints = new List<IVerletParticleConstraint>();
        }

        public void AddParticles(IEnumerable<IVerletParticle> particles)
        {
            _particles.AddRange(particles);
        }

        public void AddConstraints(IEnumerable<IVerletParticleConstraint> constraints)
        {
            _constraints.AddRange(constraints);
        }

        public void Simulate(float deltaTime)
        {
            foreach (var point in _particles)
            {
                if (point.IsPinned) continue;

                ApplyParticlesVelocity(point, deltaTime);
            }

            for (var iteration = 0; iteration < _stickResolveIterations; iteration++)
            {
                foreach (var constraint in _constraints)
                {
                    constraint.ResolveConstraint();
                }
            }
        }

        private void ApplyParticlesVelocity(IVerletParticle point, float deltaTime)
        {
            var previousPosition = point.Position;
            point.Position = point.Position * 2 - point.PreviousPosition;
            point.Position += _gravityProvider.Gravity * (deltaTime * deltaTime);
            point.PreviousPosition = previousPosition;
        }

        public void DrawGizmos()
        {
            foreach (var particle in _particles)
            {
                Gizmos.color = particle.IsPinned ? Color.red : Color.green;
                Gizmos.DrawSphere(particle.Position, .1f);
            }

            foreach (var constraint in _constraints)
            {
                constraint.DrawGizmos();
            }
        }
    }
}
