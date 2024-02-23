using Gameplay.ECS.UnityLayer;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS.Camera
{
    // TODO: Make request after player spawned
    public class GameCameraInitSystem : SimpleSystem<InitGameCameraRequest>, IUpdateSystem
    {
        protected override void Process(Entity entity, ref InitGameCameraRequest initCameraRequest, in float deltaTime)
        {
            ref TransformComponent playerTransform = ref One<PlayerControlledMarker>.Get.For<TransformComponent>();
            ref TransformComponent cameraTransform = ref entity.GetComponent<TransformComponent>();


            Vector3 cameraOffset = cameraTransform.Value.position - playerTransform.Value.position;

            entity.SetComponent(new GameCameraComponent
            {
                CameraOffset = cameraOffset,
            });

            entity.RemoveComponent<InitGameCameraRequest>();
        }
    }
}