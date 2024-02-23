using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.DI
{
    public class BaseLifetimeScope : LifetimeScope
    {
        private void OnValidate()
        {
            CollectInjectables();
        }

#if UNITY_EDITOR
        private void CollectInjectables()
        {
            IEnumerable<GameObject> interactables = FindObjectsOfType<GameObject>().Where(mb => mb.TryGetComponent(out IInjectable _));
            autoInjectGameObjects = interactables.ToList();
        }
#endif
    }
}