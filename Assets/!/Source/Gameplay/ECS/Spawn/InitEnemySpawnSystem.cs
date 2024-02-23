using System;
using System.Collections.Generic;
using Gameplay.ECS.Camera;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Infrastructure.Repos;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.ECS.Spawn
{
    public struct InitEnemySpawnRequest : IComponent
    {
        public SpawnActionData SpawnActionData;
    }

    public class InitEnemySpawnSystem : SimpleSystem<InitEnemySpawnRequest>, IUpdateSystem
    {
        private EnemyRepository _enemyRepository;

        public InitEnemySpawnSystem(EnemyRepository enemyRepository)
        {
            _enemyRepository = enemyRepository;
        }
        
        protected override void Process(Entity entity, ref InitEnemySpawnRequest request, in float deltaTime)
        {
            ref LevelComponent level = ref One<LevelComponent>.Get.Value;

            IEnumerable<Vector3> spawnPositions = request.SpawnActionData.ActionType switch
            {
                SpawnActionType.Group => SpawnGroup(request.SpawnActionData, level),
                SpawnActionType.Encircle => SpawnCircle(request.SpawnActionData, level),
                _ => throw new ArgumentOutOfRangeException()
            };

            foreach (Vector3 spawnPosition in spawnPositions)
            {
                CreatePortal(request, spawnPosition);
            }

            entity.RemoveComponent<InitEnemySpawnRequest>();
        }

        private void CreatePortal(InitEnemySpawnRequest request, Vector3 spawnPosition)
        {
            EnemyDefinition enemyDefinition = _enemyRepository.GetById(request.SpawnActionData.EnemyTypeId);
            World.CreateEntity().SetComponent(new CreatePortalRequest()
            {
                Prefab = enemyDefinition.PortalPrefab,
                Position = spawnPosition,
                SpawnRequest = new EnemySpawnRequest()
                {
                    EnemyDefinition = enemyDefinition,
                    Position = spawnPosition,
                },
            });
        }

        private IEnumerable<Vector3> SpawnGroup(SpawnActionData spawnActionData, LevelComponent level)
        {
            Vector3 position = GetRandomPosition(ref level);

            for (int i = 1; i < spawnActionData.NumberOfEnemiesToSpawn; i++)
            {
                Vector2 random = Random.insideUnitCircle * 2;
                position += new Vector3(random.x, 0, random.y);

                yield return position;
            }
        }

        private IEnumerable<Vector3> SpawnCircle(SpawnActionData spawnActionData, LevelComponent level)
        {
            Bounds levelBounds = level.Collider.bounds;
            Vector3 playerPosition = One<PlayerControlledMarker>.Get.For<TransformComponent>().Value.position;

            float radius = 5f;
            float angle = 360f / spawnActionData.NumberOfEnemiesToSpawn;

            for (int i = 0; i < spawnActionData.NumberOfEnemiesToSpawn; i++)
            {
                Vector3 position = playerPosition + new Vector3(
                    Mathf.Cos(angle * i * Mathf.Deg2Rad) * radius,
                    0,
                    Mathf.Sin(angle * i * Mathf.Deg2Rad) * radius
                );

                if (Mathf.Abs(position.x) > levelBounds.max.x)
                {
                    position.x = playerPosition.x - (position.x - playerPosition.x);
                    if (Mathf.Abs(position.x) > levelBounds.max.x)
                    {
                        position.x = Mathf.Sign(position.x) * levelBounds.max.x;
                    }
                }

                if (Mathf.Abs(position.z) > levelBounds.max.z)
                {
                    position.z = playerPosition.z - (position.z - playerPosition.z);
                    if (Mathf.Abs(position.z) > levelBounds.max.z)
                    {
                        position.z = Mathf.Sign(position.z) * levelBounds.max.z;
                    }
                }

                yield return position;
            }
        }

        private Vector3 GetRandomPosition(ref LevelComponent levelComponent)
        {
            Collider level = levelComponent.Collider;
            Bounds levelBounds = level.bounds;

            Vector3 randomInnerPoint = new(
                Random.Range(levelBounds.min.x, levelBounds.max.x),
                0,
                Random.Range(levelBounds.min.z, levelBounds.min.z)
            );

            float edgeSelection = Random.value;
            Vector3 randomEdgePoint = edgeSelection switch
            {
                < 0.25f => new Vector3(levelBounds.min.x, 0, Random.Range(levelBounds.min.z, levelBounds.max.z)),
                < 0.5f => new Vector3(levelBounds.max.x, 0, Random.Range(levelBounds.min.z, levelBounds.max.z)),
                < 0.75f => new Vector3(Random.Range(levelBounds.min.x, levelBounds.max.x), 0, levelBounds.max.z),
                _ => new Vector3(Random.Range(levelBounds.min.x, levelBounds.max.x), 0, levelBounds.max.z)
            };

            float bias = 0.7f;
            return Vector3.Lerp(randomInnerPoint, randomEdgePoint, bias);
        }
    }
}