using System;
using Gameplay.ECS.Healthcare;
using Gameplay.ECS.Spawn;
using Infrastructure.ECS;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Infrastructure.InputSystem;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using Scellecs.Morpeh.Helpers.OneFrame;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace Gameplay.ECS
{
    [Serializable]
    public struct EnemySpawnDirectorComponent : IComponent
    {
        public int CurrentWaveIndex;
        public float LastSpawnTime;
        public float SpawnInterval;
        public WaveDefinition WaveDefinition;
    }

    public class WaveSystem : SimpleSystem<GlobalWaveState> // WaveDirectorSystem, 
    {
        protected override void Process(Entity entity, ref GlobalWaveState state, in float deltaTime)
        {
            
        }
    }

    public class WaveSystemDirector
    {
        
    }

    public class WaveSystemInitializer : IInitializer
    {
        public World World { get; set; }

        public void OnAwake()
        {
            World
                .CreateEntity()
                .Set(new GlobalWaveState
                {
                    WaveIndex = 0, 
                });
        }

        public void Dispose()
        {
    
        }
    }
    
    public class CreateWaveSystem : SimpleSystem<GlobalWaveState, WaveStartRequest>
    {
        public CreateWaveSystem()
        {
            
        }

        public override void OnAwake()
        {
            base.OnAwake();
        }

        protected override void Process(Entity entity, ref GlobalWaveState state, ref WaveStartRequest waveStartRequest, in float deltaTime)
        {
            // WaveStateComponent

            entity.SetComponent(new WaveStateComponent
            {
                WaveIndex = state.WaveIndex,
            });
            
            

            entity.RemoveComponent<WaveStartRequest>();
        }
    }

    public struct WaveOnGoingMarker : IComponent
    {
        
    }
    public struct WaveStartRequest : IComponent
    {
    } 
    
    public struct WaveStateComponent : IComponent
    {
        public int WaveIndex;
        public float WaveDurationTimer;
        
        //In
        
    }
    
    public struct GlobalWaveState : IComponent
    {
        // Probably NextWaveIndex?
       public int WaveIndex;
        
    }

    // WaveSpawnLoopSystem
 
    public class EnemySpawnDirectorSystem : SimpleSystem<EnemySpawnDirectorComponent>, IUpdateSystem
    {
        private WaveDefinition _waveDefinition;

        public float WaveCounter { get; set; }


        public override void OnAwake()
        {
            _waveDefinition = Resources.Load<WaveDefinition>("WaveDefinition");
            World.CreateEntity()
                .Set(new EnemySpawnDirectorComponent
                {
                    CurrentWaveIndex = 0,
                    SpawnInterval = 5,
                    WaveDefinition = _waveDefinition
                })
                ;


            base.OnAwake();
        }

        protected override void Process(Entity entity, ref EnemySpawnDirectorComponent spawnDirector,
            in float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                InitializeWave(entity, _waveDefinition);
                return;
            }

            if (entity.Has<WaveOnGoingMarker>())
            {
                WaveCounter += deltaTime;
                TrySpawnEnemies(entity, ref spawnDirector);

                if (WaveCounter >= _waveDefinition.WaveDuration)
                {
                    int enemiesCount = World.Filter.With<EnemyComponent>().Build().GetLengthSlow();

                    if (enemiesCount == 0)
                    {
                        Debug.Log("You win!");
                        entity.RemoveComponent<WaveOnGoingMarker>();
                    }
                }
            }
            else
            {
            }
        }

        private void InitializeWave(Entity entity, WaveDefinition waveDefinition)
        {
            SpawnInterval = waveDefinition.WaveDuration / waveDefinition.EnemiesCount;
            entity.Set(new WaveOnGoingMarker());
        }

        public int SpawnInterval { get; set; }

        private void TrySpawnEnemies(Entity entity, ref EnemySpawnDirectorComponent spawnDirector)
        {
            Debug.Log("Try spawn enemies");

     

            if (WaveCounter >= _waveDefinition.WaveDuration)
            {
                SpawnBosses(entity, spawnDirector);
                return;
            }
            
            
            if (Time.time - spawnDirector.LastSpawnTime < SpawnInterval)
                return;

            spawnDirector.LastSpawnTime = Time.time;

            SpawnNormalEnemies(entity, spawnDirector);
        }

        private bool bossSpawn;

        private void SpawnBosses(Entity entity, EnemySpawnDirectorComponent spawnDirector)
        {
            if(bossSpawn)
                return;
            
            if (WaveCounter >= _waveDefinition.WaveDuration && !bossSpawn)
            {
                bossSpawn = true;
                return;
            }

            SpawnActionData spawnActionData =
                spawnDirector.WaveDefinition.BossesSpawnData[
                    Random.Range(0, spawnDirector.WaveDefinition.BossesSpawnData.Length)];
            
            
            entity.SetComponent(new InitEnemySpawnRequest
            {
                SpawnActionData = spawnActionData,
            });
        }

        private void SpawnNormalEnemies(Entity entity, EnemySpawnDirectorComponent spawnDirector)
        {
            if (SpawnedEnemeisCount >= spawnDirector.WaveDefinition.EnemiesCount)
                return;

            Debug.Log($"Spawn Normal, Was: {SpawnedEnemeisCount}");
            SpawnActionData spawnActionData =
                spawnDirector.WaveDefinition.NormalEnemiesData[
                    Random.Range(0, spawnDirector.WaveDefinition.NormalEnemiesData.Length)];
            entity.SetComponent(new InitEnemySpawnRequest
            {
                SpawnActionData = spawnActionData,
            });
            SpawnedEnemeisCount += spawnActionData.NumberOfEnemiesToSpawn;
        }

        public int SpawnedEnemeisCount { get; set; }
    }
}