using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;

namespace Gameplay.ECS
{
    [Serializable]
    public struct AutoAttackMarker : IComponent
    {
        public EntityProvider WeaponEntity;
    }
}