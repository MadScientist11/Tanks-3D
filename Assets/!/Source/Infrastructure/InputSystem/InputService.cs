using System.Collections.Generic;
using VContainer.Unity;

namespace Infrastructure.InputSystem
{
    public interface IInputService
    {
        GameplayScenario GameplayScenario { get; }
        UIScenario UIScenario { get; }
        GameScenario GameScenario { get; }
        IInputScenario ActiveScenario { get; }
        void SwitchInputScenario(InputScenario inputScenario);
        void Initialize();
    }

    public interface IInputScenario
    {
        void Enable();
        void Disable();
    }

    public class InputService : IInputService, IInitializable
    {
        private GameInput _input;
        private Dictionary<InputScenario, IInputScenario> _inputScenarios;

        private IInputScenario _activeInputScenario;

        public IInputScenario ActiveScenario => _activeInputScenario;

        public GameplayScenario GameplayScenario { get; private set; }
        public UIScenario UIScenario { get; private set; }
        public GameScenario GameScenario { get; private set; }

        public void Initialize()
        {
            _input = new GameInput();
            GameplayScenario = new GameplayScenario(_input.Gameplay);
            UIScenario = new UIScenario(_input.UI);
            GameScenario = new GameScenario(_input.Game);

            _input.UI.SetCallbacks(UIScenario);
            _input.Gameplay.SetCallbacks(GameplayScenario);
            _input.Game.SetCallbacks(GameScenario);

            GameScenario.Enable();

            _inputScenarios = new()
            {
                { InputScenario.Gameplay, GameplayScenario },
                { InputScenario.UI, UIScenario },
            };
        }

        public void SwitchInputScenario(InputScenario inputScenario)
        {
            _activeInputScenario?.Disable();
            _activeInputScenario = _inputScenarios[inputScenario];
            _activeInputScenario.Enable();
        }
    }
}