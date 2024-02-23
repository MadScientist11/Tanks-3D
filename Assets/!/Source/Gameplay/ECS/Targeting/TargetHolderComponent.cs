using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public struct TargetHolderComponent : IComponent
    {
        public EntityId TargetId;

        public TargetingType TargetingType;
        
    }
}