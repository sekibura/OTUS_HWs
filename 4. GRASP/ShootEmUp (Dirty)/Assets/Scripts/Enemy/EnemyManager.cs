using System.Collections;
using System.Collections.Generic;
using ShootEmUp.Modules.Components;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPool _enemyPool;
        [SerializeField] 
        private BulletConfig _enemyBulletConfig;
        [SerializeField]
        private BulletSystem _bulletSystem;
        
        private readonly HashSet<GameObject> m_activeEnemies = new();

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                var enemy = this._enemyPool.SpawnEnemy();
                if (enemy != null)
                {
                    if (this.m_activeEnemies.Add(enemy))
                    {
                        enemy.GetComponent<HitPointsComponent>().OnDeath += this.OnDestroyed;
                        enemy.GetComponent<EnemyAttackAgent>().OnFire += this.OnFire;
                    }    
                }
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            if (m_activeEnemies.Remove(enemy))
            {
                enemy.GetComponent<HitPointsComponent>().OnDeath -= this.OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire -= this.OnFire;

                _enemyPool.UnspawnEnemy(enemy);
            }
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.CreateBullet(new BulletSystem.Args
            {
                isPlayer = false,
                physicsLayer = (int) _enemyBulletConfig.physicsLayer,
                color = _enemyBulletConfig.color,
                damage = _enemyBulletConfig.damage,
                position = position,
                velocity = direction * _enemyBulletConfig.speed
            });
        }
    }
}