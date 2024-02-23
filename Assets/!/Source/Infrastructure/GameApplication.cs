using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class GameApplication : MonoSingleton<GameApplication>, ICoroutineRunner
    {
        public bool IsPaused { get; private set; }

        private void OnApplicationPause(bool pauseStatus)
        {
            IsPaused = pauseStatus;
        }

        public Coroutine StartRoutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void StopRoutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}