using UnityEngine;
using Zenject;

namespace Sekibura.Modules.Base
{
    public class ObjectFactory<T> : IFactory<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly DiContainer _container;

        public ObjectFactory(T prefab, Transform parent, DiContainer container)
        {
            _prefab = prefab;
            _parent = parent;
            _container = container;
        }

        public T Create()
        {
            return _container.InstantiatePrefabForComponent<T>(_prefab, _parent);
        }
    }

}