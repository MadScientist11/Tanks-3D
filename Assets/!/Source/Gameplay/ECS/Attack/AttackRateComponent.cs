using System;
using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    [Serializable]
    public struct AttackRateComponent : IComponent
    {
        public float Rate;
        public float LastAttackTime;
    }
}