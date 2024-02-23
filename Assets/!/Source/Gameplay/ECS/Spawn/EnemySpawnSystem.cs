using System;
using Gameplay.ECS.Healthcare;
using Infrastructure;
using Infrastructure.ECS;
using Infrastructure.Pool;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS.Spawn
{
    public class EnemySpawnSystem : UpdateSystem
    {
        private Filter _spawnRequests;
        private Filter _portals;

        private readonly IGameFactory _gameFactory;
        private readonly PrefabPoolService _prefabPoolService;

        public EnemySpawnSystem(IGameFactory gameFactory, PrefabPoolService prefabPoolService)
        {
            _prefabPoolService = prefabPoolService;
            _gameFactory = gameFactory;
        }

        public override void OnAwake()
        {
            _spawnRequests = World.Filter.With<EnemySpawnRequest>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _spawnRequests)
            {
                EnemySpawnRequest enemySpawnRequest = entity.GetComponent<EnemySpawnRequest>();
                _gameFactory.CreateEnemy(enemySpawnRequest.EnemyDefinition.EnemyTypeId, enemySpawnRequest.Position, Quaternion.identity);
            }

            CleanUpSpawnRequest();
        }

        private void CleanUpSpawnRequest()
        {
            foreach (Entity entity in _spawnRequests)
            {
                entity.RemoveComponent<EnemySpawnRequest>();
            }
        }
    }
}