using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    [Serializable]
    public struct LevelComponent : IComponent
    {
        public Collider Collider;
    }
}