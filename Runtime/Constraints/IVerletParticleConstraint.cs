namespace RopeSystem.Runtime.Constraints
{
    public interface IVerletParticleConstraint
    {
        void ResolveConstraint();
        void DrawGizmos();
    }
}