using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay.ECS.Healthcare
{
    public class TimedReturnToPoolSystem : SimpleSystem<TimedDestructionComponent, PoolElementMarker>
    {
        protected override void Process(Entity entity, ref TimedDestructionComponent timedDestruction, ref PoolElementMarker poolElement, in float deltaTime)
        {
            
        }
    }
}