using Infrastructure.DI;
using Infrastructure.InputSystem;
using Infrastructure.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour, IInjectable
    {
        private ISceneService _sceneService;
        private CompositionRoot _compositionRoot;
        private IInputService _inputService;

        [Inject]
        public void Construct(ISceneService sceneService, CompositionRoot compositionRoot, IInputService inputService)
        {
            _inputService = inputService;
            _compositionRoot = compositionRoot;
            _sceneService = sceneService;
        }

        private async void Start()
        {
            await _sceneService.LoadSceneAsyncInjected(SceneId.Gameplay, LoadSceneMode.Additive, _compositionRoot);
            _inputService.SwitchInputScenario(InputScenario.Gameplay);
        }
    }
}