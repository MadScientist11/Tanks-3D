using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct EnemySpawnRequest : IComponent
    {
        public EnemyDefinition EnemyDefinition;
        public Vector3 Position;
    }
}