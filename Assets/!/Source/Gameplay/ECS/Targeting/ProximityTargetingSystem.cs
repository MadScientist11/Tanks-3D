using System;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public enum TargetingState
    {
        Inactive,
        SeekingTarget,
        TrackingTarget
    }

    // TODO: switch to target that is closer
    public class ProximityTargetingSystem : SimpleSystem<DetectionRangeComponent>, IUpdateSystem
    {
        private Filter _targets;
        private Request<SetTargetRequest> _setTargetRequest;

        private TargetingState _state;

        public override void OnAwake()
        {
            _targets = World.Filter.With<ActorComponent>().Without<PlayerControlledMarker>()
                .Without<NonTargetableMarker>().Build();
            _setTargetRequest = World.GetRequest<SetTargetRequest>();

            base.OnAwake();
        }

        protected override void Process(Entity entity, ref DetectionRangeComponent detectionRange, in float deltaTime)
        {
            ref TransformComponent seekerTransform = ref entity.GetComponent<TransformComponent>();
            ref TargetHolderComponent targetHolder = ref entity.GetComponent<TargetHolderComponent>(out bool hasTarget);

            TargetingType targetingType = hasTarget ? targetHolder.TargetingType : TargetingType.Unset;

            _state = (hasTarget, targetingType) switch
            {
                (true, TargetingType.ProximityTargeting) => TargetingState.TrackingTarget,
                (false, _) => TargetingState.SeekingTarget,
                _ => TargetingState.Inactive
            };

            switch (_state)
            {
                case TargetingState.Inactive:
                    break;
                case TargetingState.SeekingTarget:
                case TargetingState.TrackingTarget:
                    GetClosestTargetFor(entity, detectionRange.Range, out EntityId targetId);
                    SetTarget(entity.ID, targetId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetTarget(EntityId entityId, EntityId newTargetId)
        {
            _setTargetRequest.Publish(new SetTargetRequest
            {
                RequesterId = entityId,
                TargetId = newTargetId,
                TargetingType = TargetingType.ProximityTargeting,
            });
        }

        private void ClearTarget(Entity entity) =>
            SetTarget(entity.ID, EntityId.Invalid);

        private bool EvaluateIsEligibleTarget(
            ref TransformComponent detectorTransform,
            ref DetectionRangeComponent detectionRange,
            Entity potentialTarget)
        {
            ref TransformComponent potentialTargetTransform = ref potentialTarget.GetComponent<TransformComponent>();
            float sqrDist = (potentialTargetTransform.Value.position - detectorTransform.Value.position).sqrMagnitude;

            return sqrDist < detectionRange.SqrRange;
        }

        public void GetClosestTargetFor(Entity entity, float withinRange, out EntityId targetId)
        {
            ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();

            if (_targets.IsEmpty())
            {
                targetId = EntityId.Invalid;
                return;
            }

            float closestDistance = float.MaxValue;
            EntityId closestTarget = EntityId.Invalid;

            foreach (Entity potentialTarget in _targets)
            {
                Vector3 direction = potentialTarget.GetComponent<TransformComponent>().Value.position -
                                    transform.Value.position;
                float distance = direction.magnitude;

                if (distance > closestDistance || distance > withinRange) continue;

                closestDistance = distance;
                closestTarget = potentialTarget.ID;
            }

            targetId = closestTarget;
        }
    }
}