using Infrastructure.ECS.Scellecs.EcsStartup;

namespace Gameplay.ECS.Spawn
{
    public class EnemySpawnFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<EnemySpawnDirectorSystem>()
                .AddUpdateSystemInjected<InitEnemySpawnSystem>()
                .AddUpdateSystemInjected<EnemyPortalSystem>()
                .AddUpdateSystemInjected<EnemySpawnSystem>();
        }
    }
}