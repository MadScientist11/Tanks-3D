using Gameplay.ECS.UnityLayer;
using Infrastructure;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    //TODO: Player data config shouldn't be quieried here for sure, this system can be generic?
    
    // Delayed with animation, how to do this? Probably mono wrapper

    public struct AimComponent : IComponent
    {
        public Transform AimPart;
        public bool Aimed;
    }
    public class AimSystem : SimpleSystem<AimComponent>,IUpdateSystem
    {
        private readonly IDataProvider _dataProvider;

        public AimSystem(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        
        protected override void Process(Entity entity, ref AimComponent aim, in float deltaTime)
        {
            Vector3 targetDirection = Vector3.forward;

            ref TargetHolderComponent targetHolder = ref entity.GetComponent<TargetHolderComponent>(out bool hasTarget);
            if (hasTarget && World.Default.TryGetEntity(targetHolder.TargetId, out Entity targetEntity))
            {
                ref TransformComponent tankTransform = ref entity.GetComponent<TransformComponent>();
                ref TransformComponent targetTransform = ref targetEntity.GetComponent<TransformComponent>();

                targetDirection = (targetTransform.Value.position - tankTransform.Value.position).normalized;
            }

            Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
            aim.AimPart.rotation = Quaternion.Lerp(aim.AimPart.rotation, lookRotation, deltaTime * _dataProvider.PlayerConfig.GunRotationSpeed);
            float angleDifference = Quaternion.Angle(aim.AimPart.rotation, lookRotation);
            aim.Aimed = angleDifference <= 1;

        }
    }
}