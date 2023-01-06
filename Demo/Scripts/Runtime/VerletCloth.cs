using System.Collections.Generic;
using RopeSystem.Runtime;
using RopeSystem.Runtime.Constraints;
using RopeSystem.Runtime.Particles;
using UnityEngine;

namespace RopeSystem.Demo.Scripts.Runtime
{
    public class VerletCloth : VerletDemo
    {
        [SerializeField] private Vector2Int m_particleCount;
        [SerializeField] private float m_particleSpacing;

        private VerletSimulation _simulation;

        private void OnEnable()
        {
            _simulation = new VerletSimulation(ConstraintSolverIterations, new UnityGravityProvider());
            m_particleList = GetParticleGrid();
            _simulation.AddParticles(m_particleList);
            var stickConstraintList = GetGridStickConstraint(m_particleList);
            _simulation.AddConstraints(stickConstraintList);
        }

        private void FixedUpdate()
        {
            _simulation.Simulate(Time.fixedDeltaTime);
        }

        private List<VerletParticle> GetParticleGrid()
        {
            var particleList = new List<VerletParticle>();
            for (var j = 0; j < m_particleCount.y; j++)
            {
                for (var i = 0; i < m_particleCount.x; i++)
                {
                    var isPinned = j == 0 && (i == 0 || i == m_particleCount.x - 1);
                    particleList.Add(new VerletParticle(new Vector3(i * m_particleSpacing, -j * m_particleSpacing), isPinned));
                }
            }

            return particleList;
        }

        private IEnumerable<IVerletParticleConstraint> GetGridStickConstraint(IReadOnlyList<IVerletParticle> particleList)
        {
            var constraintList = new List<IVerletParticleConstraint>();
            for (var j = 0; j < m_particleCount.y; j++)
            {
                for (var i = 0; i < m_particleCount.x; i++)
                {
                    if(j != m_particleCount.y - 1) // Cant connect Last Row to below particle
                        constraintList.Add(new VerletStick(GetParticle(i, j), GetParticle(i, j + 1)));

                    if (i != m_particleCount.x - 1) // Cant connect Last Column to right particle
                        constraintList.Add(new VerletStick(GetParticle(i, j), GetParticle(i + 1, j)));
                }
            }

            IVerletParticle GetParticle(int x, int y)
            {
                return particleList[y * m_particleCount.x + x];
            }

            return constraintList;
        }

        private void OnDrawGizmos()
        {
            _simulation?.DrawGizmos();
        }
    }
}