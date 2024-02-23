using UnityEngine;


namespace Gameplay
{
    [CreateAssetMenu(menuName = "Create ActorDefinition", fileName = "ActorDefinition", order = 0)]
    public class ActorDefinition : ScriptableObject
    {
        public GameObject Prefab;
    }
}