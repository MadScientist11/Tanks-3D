using Gameplay.ECS.Healthcare;
using Infrastructure;
using Infrastructure.ECS;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct CreatePortalRequest : IComponent
    {
        public GameObject Prefab;
        public Vector3 Position;
        public EnemySpawnRequest SpawnRequest;
    }

    public class EnemyPortalSystem : UpdateSystem
    {
        private Filter _createPortalRequests;
        private Filter _portals;
        
        private readonly IGameFactory _gameFactory;

        public EnemyPortalSystem(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public override void OnAwake()
        {
            _createPortalRequests = World.Filter.With<CreatePortalRequest>().Build();
            _portals = World.Filter.With<PortalComponent>().Build();
        }
     
        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity requestEntity in _createPortalRequests)
            {
                ref CreatePortalRequest createPortalRequest = ref requestEntity.GetComponent<CreatePortalRequest>();
                GameObject portalGo = _gameFactory.CreatePortal(createPortalRequest.Prefab, createPortalRequest.Position, Quaternion.identity);
                W.TryGetEntity(portalGo, out Entity portalEntity);
                ref PortalComponent portal = ref portalEntity.AddOrGet<PortalComponent>();
                portal.SpawnRequest = createPortalRequest.SpawnRequest;
                portal.DelayBeforeSpawn = 5;

                requestEntity.RemoveComponent<CreatePortalRequest>();
            }


            foreach (Entity portalEntity in _portals)
            {
                ref PortalComponent portal = ref portalEntity.GetComponent<PortalComponent>();
                portal.DelayBeforeSpawn -= deltaTime;

                if (portal.DelayBeforeSpawn <= 0)
                {
                    World.CreateEntity().SetComponent(portal.SpawnRequest);
                    portalEntity.AddComponent<DestroySelfRequest>();
                }
            }
        }
    }
}