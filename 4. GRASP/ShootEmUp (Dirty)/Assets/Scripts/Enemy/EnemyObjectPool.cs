using System.Collections;
using System.Collections.Generic;
using ShootEmUp;
using ShootEmUp.Modules.Base;
using UnityEngine;
using Zenject;

public sealed class EnemyObjectPool : ObjectPool<Enemy>
{
    [Inject] 
    private DiContainer _container;
    protected override Enemy CreateObject()
    {
        Enemy newObj = _container.InstantiatePrefabForComponent<Enemy>(_prefab, _containerTransform);
        newObj.gameObject.SetActive(false);
        _pool.Enqueue(newObj);
        return newObj;
    }
}
