using UnityEngine;
using UnityEngine.InputSystem;

namespace MiniIT.INPUT
{
    public class DesktopInput : IInputHandler
    {
        public Vector2 MousePosititon
        {
            get => Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
}
