using UnityEngine;

namespace MiniIT.GAME
{
    public partial class MergeObject
    {
        private class ConnectCellComponent : IConnectedCells
        {
            private RectTransform _rectTransform = null;
            private Cell _currentCell = null;
            
            public ConnectCellComponent(RectTransform rectTransform)
                => _rectTransform = rectTransform;
            
            public Cell CurrentCell => _currentCell;

            public void ConnectCell(Cell cell)
            {
                _currentCell = cell;
                SetPositionToCurrentCell();
            }

            public void SetPositionToCurrentCell()
                => _rectTransform.anchoredPosition = _currentCell.RectTransform.anchoredPosition;
        }
    }
}
