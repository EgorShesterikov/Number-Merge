using System;
using UnityEngine;
using Zenject;

namespace MiniIT.GAME
{
    public class GameController : ITickable, IDisposable
    {
        public event Action<int> ScoreChanged = null;
        public event Action OnEndGame = null;

        private MergeObjectSpawner _mergeObjectSpawner = null;
        private GameControllerConfig _config = null;

        private bool _isPlaying = false;
        private bool _isPaused = false;

        private float _timeSpawn = 0;
        private float _currentTimeSpawn = 0;

        private int _score = 0;
        
        public GameController(MergeObjectSpawner mergeObjectSpawner, GameControllerConfig config)
        {
            _mergeObjectSpawner = mergeObjectSpawner;
            _config = config;

            _mergeObjectSpawner.OnSpawnedMergeObject += MergeObjectTracking;
        }

        public bool IsPlaying => _isPlaying;
        public int Score => _score;

        public void Dispose()
            => _mergeObjectSpawner.OnSpawnedMergeObject -= MergeObjectTracking;

        public void StartGame()
        {
            _isPlaying = true;

            _timeSpawn = _config.StartTimeSpawnObject;
            _currentTimeSpawn = _timeSpawn;

            _score = 0;

            _mergeObjectSpawner.RandomSpawn();
            _mergeObjectSpawner.RandomSpawn();
        }
        public void StopGame()
        {
            _isPlaying = false;

            _mergeObjectSpawner.DestroyAllMergeObjects();
        }

        public void SetPause(bool isActiv)
            => _isPaused = isActiv;

        public void Tick()
        {
            if (_isPlaying == true && _isPaused == false)
            {
                _currentTimeSpawn -= Time.deltaTime;

                if(_currentTimeSpawn <= 0)
                {
                    if (_mergeObjectSpawner.RandomSpawn() == true)
                        UpdateTimeSpawn();
                    else
                        OnEndGame?.Invoke();
                }
            }
        }

        private void UpdateTimeSpawn()
        {
            float changeTimeSpawnObject = _timeSpawn * _config.AccelerationCoefficient;

            if (changeTimeSpawnObject >= _config.MinTimeSpawnSpawnObject)
                _timeSpawn = changeTimeSpawnObject;

            _currentTimeSpawn = _timeSpawn;
        }

        private void MergeObjectTracking(MergeObject mergeObject)
        {
            mergeObject.OnDestroyed += DestroyMergeObjectTracking;
            mergeObject.OnMerged += AddScoreForMerge;
        }
        private void DestroyMergeObjectTracking(MergeObject mergeObject)
        {
            mergeObject.OnDestroyed -= DestroyMergeObjectTracking;
            mergeObject.OnMerged -= AddScoreForMerge;
        }

        private void AddScoreForMerge(int score)
        {
            _score += score;
            ScoreChanged?.Invoke(_score);
        }
    }
}
