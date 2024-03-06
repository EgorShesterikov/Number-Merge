using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniIT.INPUT
{
    public class MobileInput : IInputHandler
    {
        public Vector2 MousePosititon
        {
            get => Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
        }
    }
}
