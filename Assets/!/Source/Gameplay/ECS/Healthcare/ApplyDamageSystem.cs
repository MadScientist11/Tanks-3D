using Infrastructure.ECS;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers.OneFrame;

namespace Gameplay.ECS.Healthcare
{
    public struct DamagedEvent : IComponent
    {
        public EntityId DamagedEntityId;
        public int Amount;
    }

    public class ApplyDamageSystem : UpdateSystem
    {
        private Request<DamageRequest> _damageRequest;

        public override void OnAwake()
        {
            _damageRequest = World.GetRequest<DamageRequest>();
             World.RegisterOneFrame<DamagedEvent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (DamageRequest damageRequest in _damageRequest.Consume())
            {
                World.Default.TryGetEntity(damageRequest.Reciever, out Entity reciver);

                ref HealthComponent health = ref reciver.GetComponent<HealthComponent>();

                health.Amount -= damageRequest.Amount;

                World.CreateEntity().SetComponent(new DamagedEvent
                {
                    DamagedEntityId = reciver.ID,
                    Amount = damageRequest.Amount
                });
            }
        }
    }
}