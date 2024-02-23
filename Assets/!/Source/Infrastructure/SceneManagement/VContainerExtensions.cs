using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Infrastructure.SceneManagement
{
    public static class VContainerExtensions
    {
        public static async UniTask LoadSceneAsyncInjected(this ISceneService sceneService,
            SceneId sceneId, LoadSceneMode loadSceneMode,
            LifetimeScope sceneParentScope)
        {
            using (LifetimeScope.EnqueueParent(sceneParentScope))
            {
                await sceneService.LoadSceneAsync(sceneId, loadSceneMode);
            }
        }
    }
}