using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Create PlayerConfiguration", fileName = "PlayerConfiguration", order = 0)]
    public class AvatarDefinition : ScriptableObject
    {
        public GameObject Prefab;
        public float MoveSpeed;
        public float GunRotationSpeed = 20;
    }
}