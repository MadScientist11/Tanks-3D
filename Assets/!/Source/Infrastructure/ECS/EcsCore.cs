using Gameplay;
using Gameplay.DI;
using Gameplay.ECS;
using Gameplay.ECS.Camera;
using Gameplay.ECS.Healthcare;
using Gameplay.ECS.Spawn;
using Gameplay.ECS.WorldCreation;
using Infrastructure.DI;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace Infrastructure.ECS
{
    public class EcsCore : MonoBehaviour, IInjectable
    {
        private World _world;

        private GameplayScope _gameplayScope;

        [Inject]
        public void Construct(GameplayScope gameplayScope)
        {
            _gameplayScope = gameplayScope;
        }

        private EcsStartup _startUp;


        private void Awake()
        {
            _startUp = new EcsStartup(_gameplayScope);


            // Add Prefabs pool scene?
            _startUp
                .AddSystemsGroup()
                .AddFeatureInjected<WorldCreationFeature>()
                .AddFeatureInjected<CollisionFeature>()
                .AddUpdateSystemInjected<VelocitySystem>()
                .AddFeatureInjected<ParticlesFeature>()
                .AddUpdateSystemInjected<PlayerMoveInputSystem>()
                .AddMovementFeatureSet()
                .AddWavesFeatureSet()
                .AddBattleFeatureSet()
                .AddHealthcareFeatureSet()
                .AddFeatureInjected<GameCameraFeature>()
                .AddUIFeatureSet()
                .AddLateSystemInjected<DeathSystem>()
                .AddFeatureInjected<DestructionFeature>()
                ;


            _startUp.Initialize(updateByUnity: true);
        }

        private void OnDestroy()
        {
            _startUp?.Dispose();
        }
    }

    public class ParticlesFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<PlayParticleOnContactSystem>()
                .AddUpdateSystemInjected<PlayParticleSystem>()
                .AddUpdateSystemInjected<DespawnParticlesOnStopPlayingSystem>()
                ;
        }
    }

    public class CollisionFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<CollisionInitSystem>();
        }
    }

    public class DestructionFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<DestroySelfOnContactSystem>()
                .AddUpdateSystemInjected<TimedDestructionSystem>()
                .AddCleanupSystemInjected<DestroySystem>();
        }
    }

    public class DamageFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<ProduceDamageOnContactSystem>()
                .AddUpdateSystemInjected<ApplyDamageSystem>()
                .AddLateSystemInjected<DecideIsDeadSystem>()
                ;
        }
    }

    public class KnockbackFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<KnockbackSystem>()
                .AddUpdateSystemInjected<ProduceKnockbackOnContactSystem>();
        }
    }

    public class FloatingTextFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddLateSystemInjected<DisplayFloatingTextSystem>()
                .AddLateSystemInjected<CreateFloatingTextSystem>()
                .AddLateSystemInjected<AnimateFloatingTextSystem>();
        }
    }

    public class HealthbarFeature : IEcsFeature
    {
        public void Configure(EcsStartup.FeatureBuilder builder)
        {
            builder
                .AddUpdateSystemInjected<CreateHealthBarSystem>()
                .AddUpdateSystemInjected<HealthTextUiSystem>();
        }
    }
}