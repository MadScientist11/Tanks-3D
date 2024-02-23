using Infrastructure.ECS.Scellecs.EcsStartup;

namespace Gameplay.ECS
{
    public class TargetingFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<ProximityTargetingSystem>()
                .AddUpdateSystemInjected<TargetingSystem>()
                ;
        }
    }
}