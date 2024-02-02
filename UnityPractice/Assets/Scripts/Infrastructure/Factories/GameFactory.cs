using Infrastructure.AssetManagement;
using UnityEngine;
using UnityPractice.Infrastructure.AssetManagement;

namespace UnityPractice.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }
        
        public GameObject CreateHero(GameObject at) => 
            _assets.Instantiate(AssetPath.CharacterPath, at.transform.position);

        public GameObject CreateHud() => 
            _assets.Instantiate(AssetPath.HudPath);
    }
}