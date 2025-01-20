using ShootEmUp.Modules.Base;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletsObjectPool : ObjectPool<Bullet>
    {
        [Inject]
        public BulletsObjectPool(ObjectFactory<Bullet> objectFactory, int initialSize) 
            : base(objectFactory, initialSize)
        {
        }
    }
}
