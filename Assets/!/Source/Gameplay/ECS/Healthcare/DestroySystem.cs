using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS;
using Infrastructure.Pool;
using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS.Healthcare
{
    public struct DestroySelfRequest : IComponent
    {
    }

    public struct EnemyComponent : IComponent
    {
        public EnemyTypeId EnemyTypeId;
    }

    public enum EnemyTypeId
    {
        SlimeNormal,
    }
    
    public class DestroySystem : CleanUpSystem
    {
        private Filter _toDestroy;
        private Filter _toPool;
        
        private readonly PrefabPoolService _prefabPoolService;

        public DestroySystem(PrefabPoolService prefabPoolService)
        {
            _prefabPoolService = prefabPoolService;
        }

        public override void OnAwake()
        {
            _toDestroy = World.Filter.With<DestroySelfRequest>().Without<PoolElementMarker>().Build();
            _toPool = World.Filter.With<DestroySelfRequest>().With<PoolElementMarker>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _toDestroy)
            {
                ref GameObjectComponent gameObject = ref entity.GetComponent<GameObjectComponent>();
                Object.Destroy(gameObject.Value);
                World.RemoveEntity(entity);
            } 
            
            foreach (Entity entity in _toPool)
            {
                ref GameObjectComponent gameObject = ref entity.GetComponent<GameObjectComponent>();
                _prefabPoolService.Despawn(gameObject.Value);
            } 
        }
    }
}