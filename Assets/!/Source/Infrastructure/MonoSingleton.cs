using UnityEngine;

namespace Infrastructure
{
    public abstract class MonoSingleton<T> : MonoSingleton where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Debug.LogError($"Destroying duplicate {typeof(T)} on {Instance.gameObject.name}");
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }

    }

    public abstract class MonoSingleton : MonoBehaviour
    {
      
    }
}