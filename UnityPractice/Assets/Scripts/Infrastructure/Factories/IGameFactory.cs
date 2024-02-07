using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityPractice.Character;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject HeroGameObject { get; set; }
        event Action HeroCreated;
        
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
        void CleanUp();
    }
}