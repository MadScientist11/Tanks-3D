using Infrastructure;
using Infrastructure.Pool;
using Infrastructure.Repos;
using VContainer;
using VContainer.Unity;

namespace Gameplay.DI
{
    public class GameplayScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameFactory(builder);

            builder.Register<PrefabPoolService>(Lifetime.Singleton).AsSelf();
            builder.Register<EnemyRepository>(Lifetime.Singleton).As<IInitializable>().AsSelf();
        }

        private void RegisterGameFactory(IContainerBuilder builder)
        {
            builder
                .Register<GameFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}