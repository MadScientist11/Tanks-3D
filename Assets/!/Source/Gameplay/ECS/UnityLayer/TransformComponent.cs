using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS.UnityLayer
{
    [Serializable]
    public struct TransformComponent : IComponent
    {
        public Transform Value;
    }
}