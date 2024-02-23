using Infrastructure.InputSystem;
using Infrastructure.SceneManagement;
using VContainer;

namespace Infrastructure.DI
{
    public class CompositionRoot : BaseLifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameApplication(builder);
            RegisterSceneService(builder);
            RegisterInputService(builder);

            RegisterDataProvider(builder);
        }

        private void RegisterGameApplication(IContainerBuilder builder)
        {
            builder
                .Register<GameApplication>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }

        private void RegisterSceneService(IContainerBuilder builder)
        {
            builder
                .Register<SimpleSceneLoader>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }

        private void RegisterInputService(IContainerBuilder builder)
        {
            builder
                .Register<InputService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }

        private void RegisterDataProvider(IContainerBuilder builder)
        {
            builder
                .Register<DataProvider>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}