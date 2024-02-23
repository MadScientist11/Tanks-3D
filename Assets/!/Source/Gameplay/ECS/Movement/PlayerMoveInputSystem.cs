using Infrastructure;
using Infrastructure.ECS.Scellecs.EcsStartup;
using Infrastructure.InputSystem;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Gameplay.ECS
{
    public class PlayerMoveInputSystem : SimpleSystem<PlayerControlledMarker, MovableComponent>, IUpdateSystem
    {
        private readonly IInputService _inputService;
        private readonly IDataProvider _dataProvider;

        public PlayerMoveInputSystem(IInputService inputService, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _inputService = inputService;
        }
        
        protected override void Process(Entity entity, ref PlayerControlledMarker _, ref MovableComponent movable, in float deltaTime)
        {
            Vector2 moveInput = _inputService.GameplayScenario.MoveDirection;
            movable.MoveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            movable.Speed = _dataProvider.PlayerConfig.MoveSpeed;
        }
    }
}