using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPositions _enemyPositions;
        [SerializeField]
        private EnemyObjectPool _enemyPool;
        [SerializeField] 
        private BulletConfig _enemyBulletConfig;
        [SerializeField]
        private BulletSystem _bulletSystem;
        [SerializeField]
        private GameObject _targetCharacter;

        private float _spawnDelaySec = 1f;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelaySec);
                Enemy enemy = _enemyPool.Get();
                
                if (enemy != null)
                {
                    enemy.HitPointsComponent.OnDeath += OnDestroyed;
                    enemy.AttackAgent.OnFire += OnFire;
                    InitEnemy(enemy);
                }
            }
        }

        private void InitEnemy(Enemy enemy)
        {
            Transform spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            Transform attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.MoveAgent.SetDestination(attackPosition.position);
            enemy.AttackAgent.SetTarget(_targetCharacter);
            enemy.AttackAgent.OpenFire();
        }

        private void OnDestroyed(GameObject enemyGO)
        {
            Enemy enemy;
            
            if(!enemyGO.TryGetComponent<Enemy>(out enemy)) 
                return;
            
            enemy.HitPointsComponent.OnDeath -= OnDestroyed;
            enemy.AttackAgent.OnFire -= OnFire;
            _enemyPool.ReturnToPool(enemy);
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.CreateBullet(
                position: position, 
                velocity:  direction * _enemyBulletConfig.Speed,
                config: _enemyBulletConfig
            );
        }
    }
}