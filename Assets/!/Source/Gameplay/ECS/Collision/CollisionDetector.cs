using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS.Healthcare
{
    public class CollisionDetector : MonoBehaviour
    {
        private Entity _linkedEntity;

        public void Init(World world, Entity linkedEntity)
        {
            _linkedEntity = linkedEntity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!Initialized())
                return;

            Entity otherEntity = null;

            if (W.TryGetEntity(other.gameObject, out otherEntity))
            {
            }
            else if (other.TryGetComponent(out EntityLink link))
            {
                otherEntity = link.EntityProvider.Entity;
            }

            if (otherEntity == null)
                return;
            
            World.Default.CreateEntity()
                .SetComponent(new ContactEvent
                    {
                        CollisionSourceId = _linkedEntity.ID,
                        CollideeId = otherEntity.ID
                    }
                );
        }

        private bool Initialized()
        {
            return _linkedEntity.ID != EntityId.Invalid;
        }
    }
}