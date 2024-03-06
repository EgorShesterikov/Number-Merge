namespace MiniIT.GAME
{
    public interface IConnectedCells
    {
        Cell CurrentCell { get; }

        void ConnectCell(Cell cell);
        void SetPositionToCurrentCell();
    }
}
