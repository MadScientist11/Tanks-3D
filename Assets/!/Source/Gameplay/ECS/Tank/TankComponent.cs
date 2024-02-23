using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    [Serializable]
    public struct TankComponent : IComponent
    {
        public Transform Gun;
    }
}