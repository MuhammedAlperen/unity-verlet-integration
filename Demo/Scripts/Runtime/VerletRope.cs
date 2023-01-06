using System;
using System.Collections.Generic;
using System.Linq;
using RopeSystem.Runtime;
using RopeSystem.Runtime.Constraints;
using UnityEngine;

namespace RopeSystem.Demo.Scripts.Runtime
{
    public class VerletRope : VerletDemo
    {
        [SerializeField] private List<IndexedStick> m_stickList;

        private VerletSimulation _simulation;

        private void OnEnable()
        {
            _simulation = new VerletSimulation(ConstraintSolverIterations, new UnityGravityProvider());
            _simulation.AddParticles(m_particleList);
            _simulation.AddConstraints(m_stickList.Select(GetStick).ToList());
        }

        private void FixedUpdate()
        {
            _simulation.Simulate(Time.fixedDeltaTime);
        }

        private VerletStick GetStick(IndexedStick indexedStick)
        {
            var startPoint = m_particleList[indexedStick.StartPointIndex];
            var endPoint = m_particleList[indexedStick.EndPointIndex];
            var stiffness = indexedStick.Stiffness;

            return new VerletStick(startPoint, endPoint, stiffness);
        }

        private void OnDrawGizmos()
        {
            _simulation?.DrawGizmos();
        }

        [Serializable]
        private class IndexedStick
        {
            [SerializeField] private int m_startPointIndex;
            [SerializeField] private int m_endPointIndex;
            [SerializeField] private float m_stiffness;

            public int StartPointIndex => m_startPointIndex;
            public int EndPointIndex => m_endPointIndex;
            public float Stiffness => m_stiffness;
        }
    }

    internal class UnityGravityProvider : IGravityProvider
    {
        public Vector3 Gravity => Physics.gravity;
    }
}