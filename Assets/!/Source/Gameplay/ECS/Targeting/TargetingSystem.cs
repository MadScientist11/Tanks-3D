using Scellecs.Morpeh;
using UpdateSystem = Infrastructure.ECS.UpdateSystem;

namespace Gameplay.ECS
{
    public enum TargetingType
    {
        Unset = 0,
        ProximityTargeting = 1,
    }
    
    public class TargetingSystem : UpdateSystem
    {
        private Request<SetTargetRequest> _setTargetRequest;

        public override void OnAwake()
        {
            _setTargetRequest = World.GetRequest<SetTargetRequest>();
        }

        public override void OnUpdate(float deltaTime)
        {
            HandleSetTargetRequest();
            
            //Maybe should be done through event destroy system?
            foreach (Entity entity in World.Filter.With<TargetHolderComponent>().Build())
            {
                ref TargetHolderComponent targetHolder = ref entity.GetComponent<TargetHolderComponent>();

                if (!W.Has(targetHolder.TargetId) || targetHolder.TargetId == EntityId.Invalid)
                {
                    entity.RemoveComponent<TargetHolderComponent>();
                }
            }
        }

        private void HandleSetTargetRequest()
        {
            foreach (SetTargetRequest request in _setTargetRequest.Consume())
            {
                if (request.TargetId == EntityId.Invalid)
                {
                    continue;
                }
                
                if (World.Default.TryGetEntity(request.RequesterId, out Entity targetEntity))
                {
                    targetEntity.SetComponent(new TargetHolderComponent
                    {
                        TargetId = request.TargetId,
                        TargetingType = request.TargetingType,
                    });
                }
            }
        }
    }
}