using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.UI
{
    public class MainMenu : WindowBase
    {
        public event Action ClickedPlayButton = null;
        public event Action ClickedSettingsButton = null;

        [Space]
        [SerializeField] private TextMeshProUGUI _recordText = null;

        [Space]
        [SerializeField] private Button _playButton = null;
        [SerializeField] private Button _settingsButton = null;

        public void SetRecordText(int value)
            => _recordText.text = $"(Record: {value})";

        private void Awake()
        {
            _playButton.onClick.AddListener(ClickPlayButton);
            _settingsButton.onClick.AddListener(ClickSettingsButton);
        }
        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(ClickPlayButton);
            _settingsButton.onClick.RemoveListener(ClickSettingsButton);
        }

        private void ClickPlayButton()
        {
            ShakeAnimButton(_playButton);
            ClickedPlayButton?.Invoke();
        }
        private void ClickSettingsButton()
        {
            ShakeAnimButton(_settingsButton);
            ClickedSettingsButton?.Invoke();
        }
    }
}
