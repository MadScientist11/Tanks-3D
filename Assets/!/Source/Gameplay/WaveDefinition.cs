using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.ECS
{
    [CreateAssetMenu(menuName = "Create WaveDefinition", fileName = "WaveDefinition", order = 0)]
    public class WaveDefinition : ScriptableObject
    {
        public int EnemiesCount;
        public int WaveDuration;
        [FormerlySerializedAs("SpawnActionData")] public SpawnActionData[] NormalEnemiesData;
        public SpawnActionData[] BossesSpawnData;
    }
}