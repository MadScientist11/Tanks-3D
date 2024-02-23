using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartRoutine(IEnumerator enumerator);
        
        void StopRoutine(Coroutine coroutine);
    }
}