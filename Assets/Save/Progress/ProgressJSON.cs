using System;

namespace MiniIT.SAVE
{
    [Serializable]
    public struct ProgressJSON
    {
        public int Record;

        public ProgressJSON(int record)
        {
            Record = record;
        }
    }
}