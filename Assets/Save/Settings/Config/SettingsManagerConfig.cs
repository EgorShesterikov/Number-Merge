using UnityEngine;

namespace MiniIT.SAVE
{
    [CreateAssetMenu(fileName = "SettingsManagerConfig", menuName = "Save/SettingsManagerConfig")]
    public class SettingsManagerConfig : BaseSaveConfig
    {
        [Space]
        [SerializeField, Range(0.0001f, 1f)] private float _defaultMusicVolue;
        [SerializeField, Range(0.0001f, 1f)] private float _defaultSFXVolue;

        public float DefaultMusicVolue => _defaultMusicVolue;
        public float DefaultSFXVolue => _defaultSFXVolue;
    }
}

