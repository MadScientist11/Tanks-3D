using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public static class EntityExtensions
    {
        public static Entity Set<T>(this Entity entity, T comp) where T : struct, IComponent
        {
            entity.SetComponent(comp);
            return entity;
        } 
        
        public static Entity SetIfNone<T>(this Entity entity, T comp) where T : struct, IComponent
        {
            return !entity.Has<T>() ? entity.Set(comp) : entity;
        }
    }
}