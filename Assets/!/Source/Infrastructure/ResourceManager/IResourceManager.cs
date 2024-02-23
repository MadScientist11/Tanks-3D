using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.ResourceManager
{
    public interface IResourceManager
    {
        UniTask<T> ProvideAssetAsync<T>(string path) where T : Object;
    }
}