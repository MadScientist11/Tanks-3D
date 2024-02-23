using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public class AttackRequestCleanUpSystem : ICleanupSystem
    {
        public World World { get; set; }

        public void OnAwake()
        {
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in World.Filter.With<AttackRequest>().Build())
            {
                entity.RemoveComponent<AttackRequest>();
            }
        }

        public void Dispose()
        {
            
        }
    }
}