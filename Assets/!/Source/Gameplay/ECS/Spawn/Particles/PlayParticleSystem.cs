using System.Collections.Generic;
using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Infrastructure;
using Infrastructure.ECS;
using Infrastructure.Pool;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct PlayParticleRequest : IComponent
    {
        public GameObject Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
    }

    public class PlayParticleSystem : UpdateSystem
    {
        private Filter _spawnRequests;

        private readonly Queue<PlayParticleRequest> _particleSpawnQueue = new();
        private Transform _parent;
        
        private readonly PrefabPoolService _poolingService;
        private readonly IGameFactory _gameFactory;

        public PlayParticleSystem(PrefabPoolService poolingService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _poolingService = poolingService;
        }

        public override void OnAwake()
        {
            _spawnRequests = World.Filter.With<PlayParticleRequest>().Build();
            _parent = new GameObject("Particles").transform;
        }

        // TODO: Pooling bullets
        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _spawnRequests)
            {
                ref PlayParticleRequest request = ref entity.GetComponent<PlayParticleRequest>();

                _particleSpawnQueue.Enqueue(request);

                entity.RemoveComponent<PlayParticleRequest>();
            }

            const int HardLimit = 100;

            for (int i = 0; i < HardLimit && _particleSpawnQueue.TryDequeue(out PlayParticleRequest request); i++)
            {
                GameObject particle = _gameFactory.CreateParticle(request.Prefab, _parent);
                
                particle.transform.position = request.Position;
                particle.transform.rotation = request.Rotation;
            }
        }
    }
}