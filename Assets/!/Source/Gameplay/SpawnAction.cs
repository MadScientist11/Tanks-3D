using System;
using Gameplay.ECS.Healthcare;
using UnityEngine.Serialization;

namespace Gameplay
{
    [Serializable]
    public class SpawnActionData
    {
        public EnemyTypeId EnemyTypeId;
        public SpawnActionType ActionType = SpawnActionType.Group;
        public int NumberOfEnemiesToSpawn = 1;
    }

    public enum SpawnActionType
    {
        Group,
        Encircle
    }
}