using Scellecs.Morpeh;

namespace Gameplay.ECS
{
    public struct PortalComponent : IComponent
    {
        public float DelayBeforeSpawn;
        public EnemySpawnRequest SpawnRequest;
    }
}