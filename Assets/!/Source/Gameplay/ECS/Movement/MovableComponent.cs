using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    [Serializable]
    public struct MovableComponent : IComponent
    {
        public Vector3 MoveDirection;
        public float Speed;
    }
}