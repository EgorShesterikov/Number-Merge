using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.UI
{
    public class SettingsMenu : WindowBase
    {
        private const float MIN_SLIDER_VALUE = 0.0001f;
        private const float MAX_SLIDER_VALUE = 1f;

        public event Action<float> ChangedMusicSlider = null;
        public event Action<float> ChangedSFXSlider = null;
        public event Action ClickedBackButton = null;

        [Space]
        [SerializeField] private EventSlider _musicSlider = null;
        [SerializeField] private EventSlider _sfxSlider = null;
        [SerializeField] private Button _backButton = null;

        public IPointered MusicSliderPointer => _musicSlider;
        public IPointered SFXSliderPointer => _sfxSlider;

        public float MusicSliderValue
        {
            get => _musicSlider.value;
            set 
            {
                if (value < MIN_SLIDER_VALUE || value > MAX_SLIDER_VALUE)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _musicSlider.value = value;
            }
        }
        public float SFXSliderValue
        {
            get => _sfxSlider.value;
            set 
            {
                if (value < MIN_SLIDER_VALUE || value > MAX_SLIDER_VALUE)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _sfxSlider.value = value;
            }
        }

        private void Awake()
        {
            _musicSlider.onValueChanged.AddListener(ChangeMusicSlider);
            _sfxSlider.onValueChanged.AddListener(ChangeSFXSlider);
            _backButton.onClick.AddListener(ClickBackButton);
        }
        private void OnDestroy()
        {
            _musicSlider.onValueChanged.RemoveListener(ChangeMusicSlider);
            _sfxSlider.onValueChanged.RemoveListener(ChangeSFXSlider);
            _backButton.onClick.RemoveListener(ClickBackButton);
        }

        private void ChangeMusicSlider(float volume)
            => ChangedMusicSlider?.Invoke(volume);
        private void ChangeSFXSlider(float volume)
            => ChangedSFXSlider?.Invoke(volume);

        private void ClickBackButton()
        {
            ShakeAnimButton(_backButton);
            ClickedBackButton?.Invoke();
        }
    }
}
