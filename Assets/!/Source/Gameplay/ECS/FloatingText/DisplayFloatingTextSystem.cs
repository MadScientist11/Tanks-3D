using Gameplay.ECS;
using Gameplay.ECS.Healthcare;
using Gameplay.ECS.UnityLayer;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Gameplay
{
    public class DisplayFloatingTextSystem : SimpleLateSystem<DamagedEvent>
    {
        private Request<CreateFloatingTextRequest> _createRequest;

        public override void OnAwake()
        {
            _createRequest = World.GetRequest<CreateFloatingTextRequest>();
            base.OnAwake();
        }

        protected override void Process(Entity _, ref DamagedEvent damagedEvent, in float deltaTime)
        {
            Entity damagedEntity = W.GetSafe(damagedEvent.DamagedEntityId);
            if (damagedEntity.Has<EnemyComponent>())
            {
                _createRequest.Publish(new CreateFloatingTextRequest
                {
                    WorldPosition = damagedEntity.GetComponent<TransformComponent>().Value.position,
                    Message = damagedEvent.Amount.ToString()
                });
            }
        }
    }
}