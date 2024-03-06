using UnityEngine;

namespace MiniIT.GAME
{
    [CreateAssetMenu(fileName = "MergeObjectFactoryConfig", menuName = "Factory/MergeObjectFactoryConfig")]
    public class MergeObjectFactoryConfig : ScriptableObject
    {
        [SerializeField] private MergeObject _mergeObject = null;

        public MergeObject MergeObject => _mergeObject;
    }
}
