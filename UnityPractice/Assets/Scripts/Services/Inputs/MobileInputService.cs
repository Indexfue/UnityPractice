using UnityEngine;

namespace UnityPractice
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis;
    }
}