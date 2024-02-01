using System.Collections;
using UnityEngine;

namespace UnityPractice.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}