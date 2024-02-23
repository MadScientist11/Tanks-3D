using Infrastructure;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS
{
    public class Weapon
    {
        // CooldownDuration,
        // RequiresTarget
        // WeaponId?
    }
    //Validator systems
    // Weapon ( FireRate, WeaponHolder )
    // Attack System -> Prefab(Attack Validator) -> Attack Spawner System -> 
    // Validate Fire Rate?      Produces Prefab
    // Requires Target

    public class ProjectileAttackSystem : SimpleSystem<AttackRequest>,IUpdateSystem
    {
        private readonly IGameFactory _gameFactory;

        public ProjectileAttackSystem(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        protected override void Process(Entity entity, ref AttackRequest attackRequest, in float deltaTime)
        {
            Entity weaponEntity = W.GetSafe(attackRequest.WeaponEntityId);
            if (!weaponEntity.Has<ProjectileWeaponComponent>())
            {
                return;
            }

            if (AttackUtilities.CheckForCooldown(ref weaponEntity)
                && AttackUtilities.CheckForTarget(ref entity) && entity.GetComponent<AimComponent>().Aimed)
            {
                ref ProjectileWeaponComponent ranged = ref weaponEntity.GetComponent<ProjectileWeaponComponent>();
                _gameFactory.CreateProjectile(ranged.Prefab, ranged.FirePoint.position, ranged.FirePoint.rotation, ranged);

                AttackUtilities.AttackHappened(ref weaponEntity);
            }
        }
    }
}