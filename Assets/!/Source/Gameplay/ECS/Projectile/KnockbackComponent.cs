using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct KnockbackComponent : IComponent
    {
        public Vector3 Direction;
        public float Strength;
        public float Time;
    }
}