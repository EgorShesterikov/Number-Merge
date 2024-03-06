using MiniIT.AUDIO;
using MiniIT.INPUT;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MiniIT.GAME
{
    public partial class MergeObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<int> OnMerged = null;
        public event Action<MergeObject> OnDestroyed = null;

        [SerializeField] private TextMeshProUGUI _scoreObjectText = null;
        [SerializeField] private RectTransform _rectTransform = null;
        [SerializeField] private AudioComponent _audioComponent = null;

        private event Action _onSelected = null;
        private event Action _onDeselected = null;

        private event Action<Collider2D> _onEnterObject = null;
        private event Action<Collider2D> _onExitObject = null;

        private MoveComponent _moverComponent = null;
        private MergeComponent _mergeComponent = null;
        private ConnectCellComponent _connectCellComponent = null;

        [Inject]
        public void Constructor(IInputHandler inputHandler)
        {
            _moverComponent = new MoveComponent(transform, inputHandler);

            _onSelected += _moverComponent.StartMove;
            _onDeselected += _moverComponent.StopMove;

            _mergeComponent = new MergeComponent(this);

            _onEnterObject += _mergeComponent.EnterCollider;
            _onExitObject += _mergeComponent.ExitCollider;

            _moverComponent.StoppedMove += _mergeComponent.Merge;

            _connectCellComponent = new ConnectCellComponent(_rectTransform);
        }

        public IConnectedCells ConnectedCellComponent => _connectCellComponent;
        public Cell CurrentCell => _connectCellComponent.CurrentCell;
        public int Score => _mergeComponent.Score;

        public void OnPointerDown(PointerEventData eventData)
            => _onSelected?.Invoke();
        public void OnPointerUp(PointerEventData eventData)
            => _onDeselected?.Invoke();

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (_moverComponent.IsMoving) 
                _onEnterObject?.Invoke(collision);
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if (_moverComponent.IsMoving)
                _onExitObject?.Invoke(collision);
        }

        public void Update()
            => _moverComponent.Tick();

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);

            _onSelected -= _moverComponent.StartMove;
            _onDeselected -= _moverComponent.StopMove;

            _moverComponent.StoppedMove -= _mergeComponent.Merge;
        }
    }
}
