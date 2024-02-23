using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;

namespace Infrastructure.ECS
{
    public abstract class UpdateSystem : IUpdateSystem
    {
        public World World { get; set; }

        public abstract void OnAwake();

        public abstract void OnUpdate(float deltaTime);

        public virtual void Dispose() { }
    }
}