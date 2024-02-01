﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityPractice.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        public IEnumerator LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName) 
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);

            while (!waitNextScene.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            onLoaded?.Invoke();
        }
    }
}