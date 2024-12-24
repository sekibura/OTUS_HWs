using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Modules.Base
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] 
        private T _prefab;
        [SerializeField] 
        private int _initialSize = 10;
        [SerializeField]
        private Transform _container;

        private Queue<T> _pool = new Queue<T>();

        protected virtual void Awake()
        {
            InitializePool();
        }

        /// <summary>
        /// Initializes the pool with the specified number of objects.
        /// </summary>
        private void InitializePool()
        {
            for (int i = 0; i < _initialSize; i++)
            {
                CreateObject();
            }
        }

        /// <summary>
        /// Creates a new object, adds it to the pool, and deactivates it.
        /// </summary>
        /// <returns>The created object.</returns>
        private T CreateObject()
        {
            T newObj = Instantiate(_prefab, _container);
            newObj.gameObject.SetActive(false);
            _pool.Enqueue(newObj);
            return newObj;
        }

        /// <summary>
        /// Retrieves an object from the pool. Creates a new object if the pool is empty.
        /// </summary>
        /// <returns>A pooled object.</returns>
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

        /// <summary>
        /// Returns an object to the pool and deactivates it.
        /// </summary>
        /// <param name="obj">The object to return.</param>
        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}