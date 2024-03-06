using MiniIT.INPUT;
using System;
using UnityEngine;

namespace MiniIT.GAME
{
    public partial class MergeObject
    {
        private class MoveComponent
        {
            public event Action StoppedMove = null;

            private Transform _transform = null;
            private IInputHandler _inputHandler = null;

            private bool _isMoving = false;

            public MoveComponent(Transform transform, IInputHandler inputHandler)
            {
                _transform = transform;
                _inputHandler = inputHandler;
            }

            public bool IsMoving => _isMoving;

            public void StartMove()
            {
                _isMoving = true;

                _transform.SetAsLastSibling();
            }
            public void StopMove()
            {
                _isMoving = false;

                StoppedMove?.Invoke();
            }

            public void Tick()
            {
                if (_isMoving)
                    _transform.position = _inputHandler.MousePosititon;
            }
        }
    }
}
