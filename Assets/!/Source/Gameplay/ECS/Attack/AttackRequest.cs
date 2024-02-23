using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public struct AttackRequest : IComponent
    {
        public EntityId WeaponEntityId;
    }
}