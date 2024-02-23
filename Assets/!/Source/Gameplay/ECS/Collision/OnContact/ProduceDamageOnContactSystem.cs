using Infrastructure.ECS;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS.Healthcare
{
    public struct DamageOnContactComponent : IComponent
    {
        public int Amount;
    }

    public struct DamageRequest : IRequestData
    {
        public EntityId Source;
        public EntityId Reciever;

        public int Amount;
    }

    public class ProduceDamageOnContactSystem : SimpleSystem<ContactEvent>, IUpdateSystem
    {
        private Request<DamageRequest> _damageRequest;

        public override void OnAwake()
        {
            _damageRequest = World.GetRequest<DamageRequest>();
            base.OnAwake();
        }
        
        protected override void Process(Entity entity, ref ContactEvent contactEvent, in float deltaTime)
        {
            if (World.Default.TryGetEntity(contactEvent.CollisionSourceId, out Entity damageSource))
            {
                ref DamageOnContactComponent damageOnContact = 
                    ref damageSource.GetComponent<DamageOnContactComponent>(out bool canDamage);
                    
                if (canDamage)
                {
                    World.Default.TryGetEntity(contactEvent.CollideeId, out Entity reciever);
                       
                    _damageRequest.Publish(new DamageRequest
                    {
                        Source = damageSource.ID,
                        Reciever = reciever.ID,
                        Amount = damageOnContact.Amount,
                    });
                }
            }
        }
    }
}