using ShootEmUp;
using ShootEmUp.Modules.Base;
using Zenject;

public sealed class EnemyObjectPool : ObjectPool<Enemy>
{
    [Inject]
    public EnemyObjectPool(ObjectFactory<Enemy> objectFactory, int initialSize) 
        : base(objectFactory, initialSize)
    {
    }
}
