using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.InputSystem
{
    public class GameplayScenario : GameInput.IGameplayActions, IInputScenario
    {
        public Vector2 MoveDirection { get; set; }
        
        
        private readonly GameInput.GameplayActions _gameplayActions;


        public GameplayScenario(GameInput.GameplayActions gameplayActions)
        {
            _gameplayActions = gameplayActions;
        }


        public void Enable() =>
            _gameplayActions.Enable();

        public void Disable() =>
            _gameplayActions.Disable();

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDirection = context.ReadValue<Vector2>();
        }
    }
}