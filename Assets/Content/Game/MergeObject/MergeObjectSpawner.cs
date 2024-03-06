using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace MiniIT.GAME
{
    public class MergeObjectSpawner : MonoBehaviour
    {
        public event Action<MergeObject> OnSpawnedMergeObject = null;

        private List<MergeObject> _spawnedMergeObjects = new List<MergeObject>();

        private MergeObjectFactory _factory = null;
        private GameCellsManager _cellsManager = null;

        [Inject]
        public void Construct(MergeObjectFactory factory, GameCellsManager gameCellsManager)
        {
            _factory = factory;
            _cellsManager = gameCellsManager;
        }

        public bool RandomSpawn()
        {
            Cell emptyCell = _cellsManager.FindRandomEmptyCell();

            if (emptyCell != null)
            {
                MergeObject mergeObject = _factory.Get(transform);

                emptyCell.ConnectObject(mergeObject.ConnectedCellComponent);

                mergeObject.OnDestroyed += DestroyMergeObjectTracking;

                _spawnedMergeObjects.Add(mergeObject);

                OnSpawnedMergeObject?.Invoke(mergeObject);

                return true;
            }
            else
            {
                return false;
            }
        }

        public void DestroyAllMergeObjects()
        {
            for (int i = 0; i < _spawnedMergeObjects.Count; i++)
            {
                _spawnedMergeObjects[i].CurrentCell.DisconnectObject();
                Destroy(_spawnedMergeObjects[i].gameObject);
            }

            _spawnedMergeObjects.Clear();
        }

        private void DestroyMergeObjectTracking(MergeObject mergeObject)
        {
            mergeObject.OnDestroyed -= DestroyMergeObjectTracking;

            _spawnedMergeObjects.Remove(mergeObject);
        }
    }
}
