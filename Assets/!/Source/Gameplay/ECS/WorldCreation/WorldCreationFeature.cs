using Infrastructure.ECS.Scellecs.EcsStartup;

namespace Gameplay.ECS.WorldCreation
{
    public class WorldCreationFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddInitializerInjected<PlayerSpawnSystem>();
        }
    }
}