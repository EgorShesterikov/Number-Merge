using UnityEngine;
using Zenject;

namespace MiniIT.GAME
{
    public class MergeObjectFactory
    {
        private DiContainer _container = null;

        private MergeObjectFactoryConfig _config = null;

        public MergeObjectFactory(DiContainer container, MergeObjectFactoryConfig config)
        {
            _container = container;
            _config = config;
        }

        public MergeObject Get(Transform transform)
        {
            MergeObject mergeObject = _container.InstantiatePrefabForComponent<MergeObject>(_config.MergeObject, transform);
            mergeObject.transform.localScale = Vector3.one;
            mergeObject.transform.SetAsFirstSibling();
            return mergeObject;
        }
    }
}
