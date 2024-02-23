using Gameplay.ECS.Healthcare;
using TriInspector;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Create EnemyDefinition", fileName = "EnemyDefinition", order = 0)]
    public class EnemyDefinition : ActorDefinition
    {
        [PropertyOrder(0)]
        public EnemyTypeId EnemyTypeId;

        public GameObject PortalPrefab;
    }
}