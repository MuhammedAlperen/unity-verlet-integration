using System;
using System.Collections.Generic;
using System.Linq;
using RopeSystem.Runtime;
using UnityEngine;

namespace RopeSystem.Demo.Scripts.Runtime
{
    public class Rope : MonoBehaviour
    {
        [SerializeField] private int m_stickResolveIterations;
        
        [SerializeField] private List<RopePoint> m_pointList;
        [SerializeField] private List<IndexedStick> m_stickList;

        private RopeSimulation _simulation;
        
        private void OnEnable()
        {
            _simulation = new RopeSimulation(m_stickResolveIterations, new UnityGravityProvider(), m_pointList, m_stickList.Select(GetStick).ToList());
        }

        private void FixedUpdate()
        {
            _simulation.Simulate(Time.fixedDeltaTime);
        }
        
        private RopeStick GetStick(IndexedStick indexedStick)
        {
            return new RopeStick(m_pointList[indexedStick.StartPointIndex], m_pointList[indexedStick.EndPointIndex], indexedStick.Stiffness);
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