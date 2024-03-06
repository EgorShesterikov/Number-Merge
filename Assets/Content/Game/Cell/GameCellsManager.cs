using System.Collections.Generic;
using UnityEngine;

namespace MiniIT.GAME
{
    public class GameCellsManager : MonoBehaviour
    {
        [SerializeField] private Cell[] cells = null;

        public Cell FindRandomEmptyCell()
        {
            List<Cell> emptyCells = new List<Cell>();

            for (int i = 0; i < cells.Length; i++)
                if (cells[i].IsEmpty == true)
                    emptyCells.Add(cells[i]);

            if (emptyCells.Count > 0)
            {
                int randomCell = Random.Range(0, emptyCells.Count);
                return emptyCells[randomCell];
            }
            else
            {
                return null;
            }
        }
    }
}
