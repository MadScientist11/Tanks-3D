using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.ResourceManager
{
    public class ResourcesManager : IResourceManager
    {
        public async UniTask<T> ProvideAssetAsync<T>(string path) where T : Object
        {
            return await Resources.LoadAsync<T>(path) as T;
        }

        public T[] ProvideAssets<T>(string path) where T : Object
        {
            return Resources.LoadAll<T>(path);
        }
    }
}