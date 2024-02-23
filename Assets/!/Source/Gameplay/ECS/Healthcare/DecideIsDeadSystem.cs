using Infrastructure.ECS;
using Scellecs.Morpeh;

namespace Gameplay.ECS.Healthcare
{
    public class DecideIsDeadSystem : UpdateSystem, ILateSystem
    {
        private Filter _aliveEntities;

        public override void OnAwake()
        {
            _aliveEntities = World.Filter.With<HealthComponent>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity aliveEntity in _aliveEntities)
            {
                ref HealthComponent health = ref aliveEntity.GetComponent<HealthComponent>();
                if (health.Amount <= 0)
                {
                    if (!aliveEntity.Has<DeadMarker>())
                        aliveEntity.AddComponent<DeadMarker>();
                }
            }
        }
    }
}