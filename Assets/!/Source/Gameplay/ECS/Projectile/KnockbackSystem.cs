using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    
    // Init Knockback component
    public class KnockbackSystem : SimpleSystem<KnockbackComponent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref KnockbackComponent knockback, in float deltaTime)
        {
            if (!entity.Has<VelocityComponent>())
                entity.AddComponent<VelocityComponent>();
            
            // TODO: This is overtime effect, so probably needs some generic handling for that?
            if (knockback.Time > 0)
            {
                ref VelocityComponent velocity = ref entity.GetComponent<VelocityComponent>();
                velocity.Velocity = knockback.Direction * knockback.Strength;
                knockback.Time -= deltaTime;
                return;
            }
            
            entity.RemoveComponent<KnockbackComponent>();
            entity.RemoveComponent<VelocityComponent>();
        }
    }

    public struct VelocityComponent : IComponent
    {
        public Vector3 Velocity;
    }  
    
    public class VelocitySystem : SimpleSystem<VelocityComponent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref VelocityComponent velocity, in float deltaTime)
        {
            ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
            transform.Value.position += velocity.Velocity * deltaTime;
        }
    }
}