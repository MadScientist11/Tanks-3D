using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS.Healthcare
{
    public class DeathSystem : SimpleLateSystem<DeadMarker>
    {
        protected override void Process(Entity entity, ref DeadMarker _, in float deltaTime)
        {
            if (!entity.Has<DestroySelfRequest>())
            {
                entity.AddComponent<DestroySelfRequest>();
            }
        }
    }
}