using UnityEngine;
using UnityPractice.Infrastructure.Services;

namespace UnityPractice
{
    public interface IInputService : IService
    {
        public Vector2 Axis { get; }
        public bool IsAttackButtonPressed { get; }
    }
}