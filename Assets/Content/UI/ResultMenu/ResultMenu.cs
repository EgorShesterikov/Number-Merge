using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.UI
{
    public class ResultMenu : WindowBase
    {
        public event Action ClickedMenuButton = null;

        [Space]
        [SerializeField] private TextMeshProUGUI _scoreText = null;
        [SerializeField] private Button _menuButton = null;

        public void SetScoreTest(int value)
            => _scoreText.text = $"Score: {value}";

        private void Awake()
            => _menuButton.onClick.AddListener(ClickMenuButton);
        private void OnDestroy()
            => _menuButton.onClick.RemoveListener(ClickMenuButton);

        private void ClickMenuButton()
        {
            ShakeAnimButton(_menuButton);
            ClickedMenuButton?.Invoke();
        }
    }
}
