using UnityEngine;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Infrastructure.Factories
{
    public interface IGameFactory : IService
    {
        GameObject CreateHero(GameObject at);
        GameObject CreateHud();
    }
}