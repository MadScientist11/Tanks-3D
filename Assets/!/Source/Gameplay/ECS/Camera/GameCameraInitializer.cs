using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using UnityEngine;

namespace Gameplay.ECS.Camera
{
    public class GameCameraInitializer : MonoBehaviour
    {
        [SerializeField] private EntityProvider _entityProvider;
        
        private void Awake()
        {
            if (_entityProvider.Entity.Has<GameCameraComponent>())
                _entityProvider.Entity.RemoveComponent<GameCameraComponent>();

        }

        private void Start()
        {
            _entityProvider.Entity.AddComponent<InitGameCameraRequest>();

        }
    }
}