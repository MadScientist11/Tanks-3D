using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public class MovementSystem : SimpleSystem<MovableComponent>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref MovableComponent movable, in float deltaTime)
        {
            ref TransformComponent transform = ref entity.GetComponent<TransformComponent>();
            Vector3 newPos = transform.Value.position + movable.MoveDirection * (movable.Speed * deltaTime);
            
            
            transform.Value.position = newPos;
            
            
            Vector3 lookDirection = movable.MoveDirection.sqrMagnitude > 0 ? movable.MoveDirection : transform.Value.forward;
            Quaternion newRotation = Quaternion.LookRotation(lookDirection);
            transform.Value.rotation = Quaternion.Lerp(transform.Value.rotation, newRotation, deltaTime * 10);
        }
    }
}