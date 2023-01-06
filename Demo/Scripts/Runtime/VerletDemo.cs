using System.Collections.Generic;
using RopeSystem.Runtime.Particles;
using UnityEngine;

namespace RopeSystem.Demo.Scripts.Runtime
{
    public class VerletDemo : MonoBehaviour
    {
        [SerializeField] private int m_constraintSolverIterations;
        [SerializeField] protected List<VerletParticle> m_particleList;

        protected int ConstraintSolverIterations => m_constraintSolverIterations;
    }
}