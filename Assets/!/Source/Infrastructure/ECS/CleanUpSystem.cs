using Scellecs.Morpeh;

namespace Infrastructure.ECS
{
    public abstract class CleanUpSystem : ICleanupSystem
    {
        public World World { get; set; }

        public abstract void OnAwake();

        public abstract void OnUpdate(float deltaTime);

        public virtual void Dispose() { }
    }
}