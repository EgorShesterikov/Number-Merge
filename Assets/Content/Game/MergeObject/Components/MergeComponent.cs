using UnityEngine;

namespace MiniIT.GAME
{
    public partial class MergeObject
    {
        private class MergeComponent
        {
            private const int MAX_SCORE = 1024;

            private int _score = 1;

            private MergeObject _currentMergeObject = null;
            private MergeObject _enterMergeObject = null;

            public MergeComponent(MergeObject mergeObject)
                => _currentMergeObject = mergeObject;

            public int Score => _score;
            public MergeObject EnterMergeObject => _enterMergeObject;

            public void EnterCollider(Collider2D collision)
            {
                if (collision.TryGetComponent(out MergeObject mergeObject))
                    _enterMergeObject = mergeObject;
            }
            public void ExitCollider(Collider2D collision)
            {
                if (collision.TryGetComponent(out MergeObject mergeObject))
                    if (_enterMergeObject != null)
                        _enterMergeObject = null;
            }

            public void Merge()
            {
                if (_enterMergeObject == null)
                    _currentMergeObject.ConnectedCellComponent.SetPositionToCurrentCell();
                else
                {
                    if (_score == _enterMergeObject.Score)
                    {
                        _score += _enterMergeObject.Score;
                        UpdateScoreObjectText();

                        _currentMergeObject.CurrentCell.DisconnectObject();
                        _currentMergeObject.ConnectedCellComponent.ConnectCell(_enterMergeObject.CurrentCell);

                        _currentMergeObject._audioComponent.PlaySound("Merge");

                        _currentMergeObject.OnMerged?.Invoke(_score);

                        Destroy(_enterMergeObject.gameObject);
                        CheckMaxScoreForDestroy();
                    }
                    else
                        _currentMergeObject.ConnectedCellComponent.SetPositionToCurrentCell();
                }
            }

            private void UpdateScoreObjectText()
                => _currentMergeObject._scoreObjectText.text = _score.ToString();
            private void CheckMaxScoreForDestroy()
            {
                if (_score >= MAX_SCORE)
                    Destroy(_currentMergeObject.gameObject);
            }
        }
    }
}
