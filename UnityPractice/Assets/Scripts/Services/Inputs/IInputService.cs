using UnityEngine;

namespace UnityPractice
{
    public interface IInputService
    {
        public Vector2 Axis { get; }
        public bool IsAttackButtonPressed { get; }
    }
}