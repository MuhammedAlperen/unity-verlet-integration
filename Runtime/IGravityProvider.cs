using UnityEngine;

namespace RopeSystem.Runtime
{
    public interface IGravityProvider
    {
        Vector3 Gravity { get; }
    }
}