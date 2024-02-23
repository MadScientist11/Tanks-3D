using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public class SimpleSceneLoader : ISceneService
    {
        public async UniTask LoadSceneAsync(SceneId sceneId, LoadSceneMode loadSceneMode)
        {
            await SceneManager.LoadSceneAsync((int)sceneId, loadSceneMode);
        }

        public async UniTask UnloadSceneAsync(SceneId sceneId)
        {
            await SceneManager.UnloadSceneAsync((int)sceneId);
        }
    }
}