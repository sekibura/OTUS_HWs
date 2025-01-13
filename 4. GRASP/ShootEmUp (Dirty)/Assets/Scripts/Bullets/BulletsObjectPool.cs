using ShootEmUp.Modules.Base;
using Zenject;


namespace ShootEmUp
{
    public sealed class BulletsObjectPool : ObjectPool<Bullet>
    {
        [Inject] 
        private DiContainer _container;
        protected override Bullet CreateObject()
        {
            Bullet newObj = _container.InstantiatePrefabForComponent<Bullet>(_prefab, _containerTransform);
            newObj.gameObject.SetActive(false);
            _pool.Enqueue(newObj);
            return newObj;
        }
    }
}
