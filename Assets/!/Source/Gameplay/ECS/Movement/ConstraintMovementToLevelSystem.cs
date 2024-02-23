using Gameplay.ECS.Camera;
using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public class ConstraintActorMovementToLevelSystem : SimpleSystem<ActorComponent, MovableComponent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref ActorComponent actor, ref MovableComponent movable,
            in float deltaTime)
        {
            Collider level = One<LevelComponent>.Get.Value.Collider;
            ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();


            if (transform.Value.position.x > level.bounds.max.x && movable.MoveDirection.x > 0 ||
                transform.Value.position.x < level.bounds.min.x && movable.MoveDirection.x < 0)
                movable.MoveDirection.x = 0;

            if (transform.Value.position.z > level.bounds.max.x && movable.MoveDirection.z > 0 ||
                transform.Value.position.z < level.bounds.min.z && movable.MoveDirection.z < 0)
                movable.MoveDirection.z = 0;
        }
    }
}