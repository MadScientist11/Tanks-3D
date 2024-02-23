using Infrastructure.ECS;
using Scellecs.Morpeh;

namespace Gameplay.ECS.Healthcare
{
    public class TimedDestructionSystem : UpdateSystem
    {
        private Filter _lifespans;
        
        public override void OnAwake()
        {
            _lifespans = World.Filter.With<TimedDestructionComponent>().Without<PoolElementMarker>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _lifespans)
            {
                ref TimedDestructionComponent timedDestruction = ref entity.GetComponent<TimedDestructionComponent>();
                
                timedDestruction.RemainingTime -= deltaTime;

                if (timedDestruction.RemainingTime <= 0f)
                {
                    if (!entity.Has<DestroySelfRequest>())
                        entity.AddComponent<DestroySelfRequest>();
                }
            }
        }
    }
}