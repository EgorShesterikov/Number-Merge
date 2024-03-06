using UnityEngine;

namespace MiniIT.GAME
{
    [CreateAssetMenu(fileName = "GameControllerConfig", menuName = "GameConfig/GameControllerConfig")]
    public class GameControllerConfig : ScriptableObject
    {
        [SerializeField] private float _startTimeSpawnObject = 2f;

        [Space, Range(0.5f, 1)]
        [SerializeField] private float _accelerationCoefficient = 0.75f;

        [Space]
        [SerializeField, Min(0.1f)] private float _minTimeSpawnObject = 0.5f;

        public float StartTimeSpawnObject => _startTimeSpawnObject;
        public float AccelerationCoefficient => _accelerationCoefficient;
        public float MinTimeSpawnSpawnObject => _minTimeSpawnObject;
    }
}
