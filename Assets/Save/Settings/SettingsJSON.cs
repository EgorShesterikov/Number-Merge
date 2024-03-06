using System;

namespace MiniIT.SAVE
{
    [Serializable]
    public struct SettingsJSON
    {
        public float Music;
        public float SFX;

        public SettingsJSON(float music, float sfx)
        {
            Music = music;
            SFX = sfx;
        }
    }
}