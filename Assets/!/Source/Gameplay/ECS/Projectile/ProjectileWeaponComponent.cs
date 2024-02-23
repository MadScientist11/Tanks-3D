using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS
{
    public struct ProjectileWeaponComponent : IComponent
    {
        public GameObject Prefab;
        public Transform FirePoint;
        public float ShotSpeed;
    }
}