using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MiniIT.UI
{
    public class EventSlider : Slider, IPointered
    {
        public event Action PointerUp;
        public event Action PointerDown;

        public override void OnPointerDown(PointerEventData eventData)
        {
            PointerDown?.Invoke();

            base.OnPointerDown(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke();

            base.OnPointerUp(eventData);
        }
    }
}
