using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS
{
    public class ProduceKnockbackOnContactSystem : SimpleSystem<ContactEvent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref ContactEvent contactEvent, in float deltaTime)
        {
            Collision.OnContact(in contactEvent, ProduceKnockbackIfMarked);
        }

        private void ProduceKnockbackIfMarked(ContactEvent contactEvent, Entity source, Entity collidee)
        {
            if (!source.Has<KnockbackOnContactMarker>())
                return;

            ref TransformComponent transform = ref source.GetComponent<TransformComponent>();
            collidee.SetComponent(new KnockbackComponent
            {
                Direction = transform.Value.forward,
                Time = .25f,
                Strength = 10,
            });
        }
    }
}