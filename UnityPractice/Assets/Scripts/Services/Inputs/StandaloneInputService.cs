using UnityEngine;

namespace UnityPractice
{
    public class StandaloneInputService : InputService
    {
        public Vector2 UnityAxis => new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis;

                if (axis == Vector2.zero) 
                {
                    axis = UnityAxis;
                }

                return axis;
            }
        }
    }
}