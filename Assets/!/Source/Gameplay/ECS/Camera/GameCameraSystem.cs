using Gameplay.ECS.UnityLayer;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS.Camera
{
    public class GameCameraSystem : SimpleLateSystem<GameCameraComponent>
    {
        private float edgeDistance = 5;

        protected override void Process(Entity entity, ref GameCameraComponent gameCamera, in float deltaTime)
        {
            ref TransformComponent playerTransform = ref One<PlayerControlledMarker>.Get.For<TransformComponent>();
            ref TransformComponent cameraTransform = ref One<GameCameraComponent>.Get.For<TransformComponent>();

            Collider level = One<LevelComponent>.Get.Value.Collider;

            Vector3 cameraWishPosition = playerTransform.Value.position + gameCamera.CameraOffset;

            if (playerTransform.Value.position.x <= level.bounds.min.x + edgeDistance ||
                playerTransform.Value.position.x >= level.bounds.max.x - edgeDistance)
            {
                cameraWishPosition =
                    new Vector3(cameraTransform.Value.position.x, cameraWishPosition.y, cameraWishPosition.z);
            }

            if (playerTransform.Value.position.z <= level.bounds.min.z + edgeDistance ||
                playerTransform.Value.position.z >= level.bounds.max.z - edgeDistance)
            {
                cameraWishPosition =
                    new Vector3(cameraWishPosition.x, cameraWishPosition.y, cameraTransform.Value.position.z);
            }

            cameraTransform.Value.position = cameraWishPosition;
        }
    }
}