using System;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    [Serializable]
    public struct ParticleOnContactComponent : IComponent
    {
        public ParticleSystem Prefab;
    }
}