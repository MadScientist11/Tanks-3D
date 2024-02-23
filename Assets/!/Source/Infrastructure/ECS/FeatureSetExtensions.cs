using Gameplay;
using Gameplay.ECS;
using Gameplay.ECS.Healthcare;
using Gameplay.ECS.Spawn;
using Infrastructure.ECS.Scellecs.EcsStartup;

namespace Infrastructure.ECS
{
    public static class FeatureSetExtensions
    {
        public static EcsStartup.StartupBuilder AddWavesFeatureSet(this EcsStartup.StartupBuilder startupBuilder)
        {
           return startupBuilder
               .AddFeatureInjected<EnemySpawnFeature>()
               ;
        } 
        
        public static EcsStartup.StartupBuilder AddBattleFeatureSet(this EcsStartup.StartupBuilder startupBuilder)
        {
            return startupBuilder
                .AddFeatureInjected<TargetingFeature>()
                .AddUpdateSystemInjected<AimSystem>()
                .AddUpdateSystemInjected<AutoAttackSystem>()
                .AddUpdateSystemInjected<ProjectileAttackSystem>()
                .AddFeatureInjected<KnockbackFeature>()
                .AddCleanupSystemInjected<AttackRequestCleanUpSystem>();
                ;
        } 
        
        
        public static EcsStartup.StartupBuilder AddUIFeatureSet(this EcsStartup.StartupBuilder startupBuilder)
        {
            return startupBuilder
                .AddFeatureInjected<FloatingTextFeature>()
                .AddFeatureInjected<HealthbarFeature>()
                ;
        }
        
     
        public static EcsStartup.StartupBuilder AddMovementFeatureSet(this EcsStartup.StartupBuilder startupBuilder)
        {
            return startupBuilder
                .AddUpdateSystem(new ConstraintActorMovementToLevelSystem())
                .AddUpdateSystem(new MovementSystem());
        }    
        
        public static EcsStartup.StartupBuilder AddHealthcareFeatureSet(this EcsStartup.StartupBuilder startupBuilder)
        {
            return startupBuilder
                    .AddFeatureInjected<DamageFeature>()
                    //.AddUpdateSystemInjected<ReturnToPoolOnDeathSystem>()
                    ;
        }
        
       
    }
}