using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Gameplay.ECS.Healthcare
{
    public class EntityLink : MonoBehaviour
    {
        [field: SerializeField] public EntityProvider EntityProvider {get; private set; }
    }
}