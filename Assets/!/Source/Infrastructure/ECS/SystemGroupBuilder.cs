using Scellecs.Morpeh;
using VContainer;
using VContainer.Unity;

namespace Infrastructure.ECS
{
    public readonly struct SystemGroupBuilder
    {
        private readonly LifetimeScope _lifetimeScope;
        private readonly SystemsGroup _systemsGroup;

        public SystemGroupBuilder(LifetimeScope scope)
        {
            _systemsGroup = World.Default.CreateSystemsGroup();
            this._lifetimeScope = scope;
        }

        public SystemGroupBuilder AddUpdateSystemInjected<T>() where T : class, ISystem
        {
            _systemsGroup.AddSystem<T>(_lifetimeScope.Container.Resolve<T>());
            return this;
        }
        
        public SystemGroupBuilder AddUpdateSystem<T>() where T : class, ISystem, new()
        {
            _systemsGroup.AddSystem<T>(new T());
            return this;
        }

        public SystemsGroup Build()
        {
            return _systemsGroup;
        }
    }
}