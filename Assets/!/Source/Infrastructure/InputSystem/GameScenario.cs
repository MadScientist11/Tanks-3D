namespace Infrastructure.InputSystem
{
    public class GameScenario : GameInput.IGameActions, IInputScenario
    {
        private GameInput.GameActions _gameActions;

        public GameScenario(GameInput.GameActions gameActions)
        {
            _gameActions = gameActions;
        }

        public void Enable() =>
            _gameActions.Enable();

        public void Disable() =>
            _gameActions.Disable();

        
    }
}