using System.Collections;
using System.Collections.Generic;

namespace RopeSystem.Runtime
{
    public class RopeSimulation
    {
        private readonly int _stickResolveIterations;
        private readonly IGravityProvider _gravityProvider;

        private readonly List<RopePoint> _points;
        private readonly List<RopeStick> _sticks;
        
        public IReadOnlyList<RopePoint> PointList => _points;
        public IReadOnlyList<RopeStick> StickList => _sticks;

        public RopeSimulation(int stickResolveIteration, IGravityProvider gravityProvider, List<RopePoint> points, List<RopeStick> sticks)
        {
            _stickResolveIterations = stickResolveIteration;
            _gravityProvider = gravityProvider;

            _points = points;
            _sticks = sticks;
        }

        public void Simulate(float deltaTime)
        {
            foreach (var point in _points)
            {
                if (point.IsPinned) continue;
                
                var previousPosition = point.Position;
                point.Position = point.Position * 2 - point.PreviousPosition;
                point.Position += _gravityProvider.Gravity * (deltaTime * deltaTime);
                point.PreviousPosition = previousPosition;
            }

            for (var iteration = 0; iteration < _stickResolveIterations; iteration++)
            {
                foreach (var stick in _sticks)
                {
                    var delta = stick.EndPoint.Position - stick.StartPoint.Position;
                    var distance = delta.magnitude;
                    var error = stick.Length - distance;
                    var percent = error / distance / 2;
                    var correction = delta * (percent * stick.Stiffness);

                    if (!stick.StartPoint.IsPinned) stick.StartPoint.Position -= correction;
                    if (!stick.EndPoint.IsPinned) stick.EndPoint.Position += correction;
                }
            }
        }
    }
}
