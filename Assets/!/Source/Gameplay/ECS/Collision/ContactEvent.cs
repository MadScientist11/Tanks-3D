using Scellecs.Morpeh;

namespace Gameplay.ECS.Healthcare
{
    public struct ContactEvent : IComponent
    {
        public EntityId CollisionSourceId;
        public EntityId CollideeId;
    }
}