using Infrastructure.ECS.Scellecs.EcsStartup;

namespace Gameplay.ECS.Camera
{
    public class GameCameraFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<GameCameraInitSystem>()
                .AddLateSystemInjected<GameCameraSystem>();
        }
    }
}