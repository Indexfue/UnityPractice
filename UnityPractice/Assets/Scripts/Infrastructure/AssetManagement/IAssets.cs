using UnityEngine;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice.Infrastructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}