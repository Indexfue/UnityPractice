using UnityEngine;

namespace UnityPractice
{
    public abstract class InputService : IInputService
    {
        protected readonly string Horizontal = "Horizontal";
        protected readonly string Vertical = "Vertical";
        protected readonly string Fire = "Fire";

        protected float HorizonalAxis => SimpleInput.GetAxis(Horizontal);
        protected float VerticalAxis => SimpleInput.GetAxis(Vertical);
        protected Vector2 SimpleInputAxis => new Vector2(HorizonalAxis, VerticalAxis);


        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonPressed => SimpleInput.GetButtonUp(Fire);
    }
}