using UnityEngine;

namespace ShootEmUp.Modules.Base
{
    public interface IObjectPool<T> where T : Component 
    {
        T Get();
        void ReturnToPool(T obj);
    }
}
