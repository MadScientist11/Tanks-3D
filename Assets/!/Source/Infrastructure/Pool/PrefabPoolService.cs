using System;
using System.Collections.Generic;
using Gameplay.ECS;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Infrastructure.Pool
{
    public class GameObjectPool
    {
        private readonly IObjectPool<GameObject> _pool;

        public int CountInactive => _pool.CountInactive;

        private readonly Queue<GameObject> _queuedInstances = new();

        public GameObjectPool()
        {
            _pool = new ObjectPool<GameObject>(OnCreateElement, OnGetElement, OnReleaseElement, OnDestroyElement);
        }

        public GameObject Get() =>
            _pool.Get();

        public PooledObject<GameObject> Get(out GameObject v) =>
            _pool.Get(out v);

        public void Release(GameObject element) =>
            _pool.Release(element);

        public void Clear()
        {
            _pool.Clear();

            while (_queuedInstances.TryDequeue(out GameObject element))
            {
                OnDestroyElement(element);
            }
        }

        public void Enqueue(GameObject element)
        {
            _queuedInstances.Enqueue(element);
        }

        private GameObject OnCreateElement()
        {
            if (_queuedInstances.TryDequeue(out GameObject instance))
            {
                return instance;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        protected virtual void OnGetElement(GameObject element)
        {
            element.SetActive(true);
        }

        protected virtual void OnReleaseElement(GameObject element)
        {
            W.TryGetEntity(element, out Entity entity);
            entity.Dispose();
            element.SetActive(false);
        }

        protected virtual void OnDestroyElement(GameObject element)
        {
            Object.Destroy(element);
        }
    }


    public class PrefabPoolService
    {
        private readonly Dictionary<int, GameObjectPool> _poolByPrefabId = new();
        private readonly Dictionary<int, GameObjectPool> _poolByInstanceId = new();


        public GameObject Spawn(GameObject prefab, Vector3 position)
        {
            GameObject element = Spawn(prefab);
            element.transform.position = position;
            return element;
        }

        public bool Has(GameObject prefab)
        {
            if (!_poolByPrefabId.TryGetValue(prefab.GetInstanceID(), out GameObjectPool pool))
            {
                return false;
            }

            return pool.CountInactive > 0;
        }

        public void Register(GameObject prefab, GameObject instance)
        {
            if (!_poolByPrefabId.TryGetValue(prefab.GetInstanceID(), out GameObjectPool pool))
            {
                pool = AddPool(prefab);
            }

            _poolByInstanceId.Add(instance.GetInstanceID(), pool);
            pool.Enqueue(instance);
            TagAsPoolElement(instance);
        }

        public GameObject Spawn(GameObject prefab, Transform parent = null)
        {
            if (!_poolByPrefabId.TryGetValue(prefab.GetInstanceID(), out GameObjectPool pool))
            {
                pool = AddPool(prefab);
            }

            GameObject element = pool.Get();
            _poolByInstanceId.Add(element.GetInstanceID(), pool);
            TagAsPoolElement(element);
            return element;
        }

        public void Despawn(GameObject element)
        {
            int id = element.GetInstanceID();
            if (_poolByInstanceId.TryGetValue(id, out GameObjectPool pool))
            {
                _poolByInstanceId.Remove(id);
                pool.Release(element);
            }
        }

        private GameObjectPool AddPool(GameObject prefab)
        {
            GameObjectPool newPool = new();
            _poolByPrefabId.Add(prefab.GetInstanceID(), newPool);
            return newPool;
        }

        private void TagAsPoolElement(GameObject obj)
        {
            W.TryGetEntity(obj, out Entity entity);
            entity.SetIfNone(new PoolElementMarker());
        }
    }
}