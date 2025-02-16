using UnityEngine;

namespace Sekibura.Modules.Base
{
    public interface IObjectPool<T> where T : Component 
    {
        T Get();
        void ReturnToPool(T obj);
    }
}
