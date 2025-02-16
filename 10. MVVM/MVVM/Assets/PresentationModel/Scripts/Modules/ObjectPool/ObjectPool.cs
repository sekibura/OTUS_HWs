using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sekibura.Modules.Base
{
    public abstract class ObjectPool<T> : IObjectPool<T> where T : Component
    {
        private ObjectFactory<T> _objectFactory;
        private int _initialSize = 10;
        protected Queue<T> _pool = new Queue<T>();
        
        [Inject]
        protected ObjectPool(ObjectFactory<T> objectFactory, int initialSize)
        {
            _objectFactory = objectFactory;
            _initialSize = initialSize;
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                CreateObject();
            }
        }
        
        protected virtual T CreateObject()
        {
            T newObj = _objectFactory.Create();
            newObj.gameObject.SetActive(false);
            _pool.Enqueue(newObj);
            return newObj;
        }
        
        public T Get()
        {
            if (_pool.Count == 0)
            {
                CreateObject();
            }

            T obj = _pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        
        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}