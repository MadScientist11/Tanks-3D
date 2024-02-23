using Scellecs.Morpeh;
using VContainer.Unity;

namespace Infrastructure.ECS
{
    public static class SystemGroupExtensions
    {
        public static SystemGroupBuilder GetSystemsGroupBuilder(this World world, LifetimeScope lifetimeScope)
        {
            return new SystemGroupBuilder(lifetimeScope);
        }
    }
}