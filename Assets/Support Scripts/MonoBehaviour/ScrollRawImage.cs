using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.SUPPORT
{
    [RequireComponent(typeof(RawImage))]
    public class ScrollRawImage : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float _scrollSpeed = 0.1f;

        [SerializeField, Range(-1, 1)] private float _directionX = 1f;
        [SerializeField, Range(-1, 1)] private float _directionY = 1f;

        [Space]
        [SerializeField] private RawImage _rawImage;

        private void Update()
        {
            Rect rect = _rawImage.uvRect;
            rect.x += _directionX * _scrollSpeed * Time.deltaTime;
            rect.y += _directionY * _scrollSpeed * Time.deltaTime;

            _rawImage.uvRect = rect;
        }
    }
}
