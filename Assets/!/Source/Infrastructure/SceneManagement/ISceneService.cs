using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public interface ISceneService
    {
        UniTask LoadSceneAsync(SceneId sceneId, LoadSceneMode loadSceneMode);
        UniTask UnloadSceneAsync(SceneId sceneId);
    }
}