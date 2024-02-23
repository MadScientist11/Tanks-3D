using Scellecs.Morpeh;
using UnityEngine;

namespace Gameplay.ECS.Camera
{
    public struct GameCameraComponent : IComponent
    {
        public Vector3 CameraOffset;
    }

    public struct InitGameCameraRequest : IComponent
    {
        
    }
}