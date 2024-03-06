using System;

namespace MiniIT.UI
{
    public interface IPointered
    {
        event Action PointerDown;
        event Action PointerUp;
    }
}
