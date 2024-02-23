using UnityEngine;

namespace Gameplay.ECS
{
    [CreateAssetMenu(menuName = "Create WeaponConfiguration", fileName = "WeaponConfiguration", order = 0)]
    public class WeaponConfiguration : ScriptableObject
    {
        public GameObject Prefab;
        public float ShotSpeed = 20;
    }
}