using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Infrastructure;
using Infrastructure.ECS;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct PoolElementMarker : IComponent
    {
    }

    public class PlayParticleOnContactSystem : SimpleSystem<ContactEvent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref ContactEvent contactEvent, in float deltaTime)
        {
            Collision.OnContact(in contactEvent, PlayParticleIfMarked);
        }

        private void PlayParticleIfMarked(ContactEvent contactEvent, Entity source, Entity colidee)
        {
            ParticleOnContactComponent particleOnContact =
                source.GetComponent<ParticleOnContactComponent>(out bool playParticle);

            if (playParticle)
            {
                ref TransformComponent transform = ref source.GetComponent<TransformComponent>();
                source.SetComponent(new PlayParticleRequest()
                {
                    Prefab = particleOnContact.Prefab.gameObject,
                    Position = transform.Value.position,
                    Rotation = transform.Value.rotation,
                });
            }
        }
    }
}