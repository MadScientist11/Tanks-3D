using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS.Healthcare
{
    public class DestroySelfOnContactSystem : SimpleSystem<ContactEvent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref ContactEvent contactEvent, in float deltaTime)
        {
            DestroyOnContactIfMarked(contactEvent.CollisionSourceId);
        }

        private void DestroyOnContactIfMarked(EntityId entityId)
        {
            Entity entity = W.GetSafe(entityId);

            if (entity.Has<DestroySelfOnContactMarker>()
                && !entity.Has<DestroySelfRequest>())
            {
                entity.AddComponent<DestroySelfRequest>();
            }
        }
    }
}