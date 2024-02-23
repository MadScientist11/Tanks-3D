using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS
{
    public struct CanAutoAttackMarker : IComponent
    {
    }


    // Handle fx side effects, audio


    // SpawnParticleOnAttack
    // SpawnParticleOnAttackCollision
    // SpawnParticleOnAttackEnd

    // if can attack send attack request, e.g if AutoAttacking is allowed, and no debuffs that make you stop attacking are active
    public class AttackValidatorSystem : SimpleSystem<CanAutoAttackMarker>
    {
        protected override void Process(Entity entity, ref CanAutoAttackMarker canAutoAttack, in float deltaTime)
        {
        }
    }
    // getting components that implement some interface?

    // AutoAttack System

    // Do an event for AutoAttack System so that the timer would be reseted in here too?
    // So no, Attack Delay should probably be another component for validators
    public class AutoAttackSystem : SimpleSystem<AutoAttackMarker>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref AutoAttackMarker autoAttack, in float deltaTime)
        {
            if (!entity.Has<TargetHolderComponent>())
            {
                return;
            }

            if (!entity.Has<AttackRequest>())
                entity.SetComponent(new AttackRequest
                {
                    WeaponEntityId = autoAttack.WeaponEntity.Entity.ID,
                });
        }
    }
}