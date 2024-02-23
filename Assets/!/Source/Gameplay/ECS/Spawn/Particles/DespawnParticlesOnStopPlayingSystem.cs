using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Infrastructure.Pool;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS
{
    public class DespawnParticlesOnStopPlayingSystem : SimpleSystem<ParticleComponent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref ParticleComponent particleComponent, in float deltaTime)
        {
            if (!particleComponent.ParticleSystem.isPlaying)
                entity.AddComponent<DestroySelfRequest>();
        }
    }
}