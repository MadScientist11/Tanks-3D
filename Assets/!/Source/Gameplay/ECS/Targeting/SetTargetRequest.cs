using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public struct SetTargetRequest : IRequestData
    {
        public EntityId RequesterId;
        public EntityId TargetId;

        public TargetingType TargetingType;
    }
}