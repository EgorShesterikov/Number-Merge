using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.UI
{
    public class PlayMenu : WindowBase
    {
        public event Action ClickedSettingsButton = null;
        public event Action ClickedSurrendButton = null;

        [Space]
        [SerializeField] private TextMeshProUGUI _scoreText = null;

        [Space]
        [SerializeField] private Button _settingsButton = null;
        [SerializeField] private Button _surrendButton = null;

        public void SetScoreText(int value)
            => _scoreText.text = $"Score: {value}";

        public override void Open()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.SetUpdate(true)
                .PrependCallback(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                    _canvasGroup.alpha = 0;

                    transform.localScale = Vector3.zero;

                    gameObject.SetActive(true);
                })
                .Append(_canvasGroup.DOFade(1, OPEN_CLOSE_DURATION).SetEase(Ease.InCirc))
                .Join(transform.DOScale(1, OPEN_CLOSE_DURATION).SetEase(Ease.InCirc))
                .AppendCallback(() => _canvasGroup.blocksRaycasts = true);
        }
        public override void Close()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.SetUpdate(true)
                .PrependCallback(() =>
                {
                    _canvasGroup.blocksRaycasts = false;
                    _canvasGroup.alpha = 1;

                    transform.localScale = Vector3.one;

                    gameObject.SetActive(true);
                })
                .Append(_canvasGroup.DOFade(0, OPEN_CLOSE_DURATION).SetEase(Ease.InCirc))
                .Join(transform.DOScale(0, OPEN_CLOSE_DURATION).SetEase(Ease.InCirc))
                .AppendCallback(() => gameObject.SetActive(false));
        }

        private void Awake()
        {
            _settingsButton.onClick.AddListener(ClickSettingsButton);
            _surrendButton.onClick.AddListener(ClickSurrendButton);
        }
        private void OnDestroy()
        {
            _settingsButton.onClick.RemoveListener(ClickSettingsButton);
            _surrendButton.onClick.RemoveListener(ClickSurrendButton);
        }

        private void ClickSettingsButton()
        {
            ShakeAnimButton(_settingsButton);
            ClickedSettingsButton?.Invoke();
        }
        private void ClickSurrendButton()
        {
            ShakeAnimButton(_surrendButton);
            ClickedSurrendButton?.Invoke();
        }
    }
}
