using UnityEngine;

namespace MiniIT.GAME
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private bool _isEmpty = true;

        public RectTransform RectTransform => _rectTransform;
        public bool IsEmpty => _isEmpty;

        public void ConnectObject(IConnectedCells connectObject)
        {
            _isEmpty = false;
            connectObject.ConnectCell(this);
        }
        public void DisconnectObject()
            => _isEmpty = true;

    }
}
