using System;
using System.Collections.Generic;
using ShootEmUp.Modules.Base;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [Inject]
        private IObjectPool<Bullet> _bulletPool;
        
        private List<Bullet> _activeBullets = new List<Bullet>();
        
        [Inject]
        private GameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine.AddListener(GameStatesNames.InitializationStateName, onEnter: OnInitializationStateEnter);
        }

        private void OnDestroy()
        {
            _gameStateMachine.RemoveListener(GameStatesNames.InitializationStateName, onEnter: OnInitializationStateEnter);
        }

        private void OnInitializationStateEnter()
        {
            while (_activeBullets.Count > 0)
            {
                RemoveBullet(_activeBullets[0]);
            }
        }

        public void CreateBullet(Vector2 position, Vector2 velocity, BulletConfig config)
        {
            var bullet = _bulletPool.Get();
            bullet.InitBullet(position, velocity, config);
            _activeBullets.Add(bullet);
            bullet.OnCollisionEntered += OnBulletCollisionEnter;
            bullet.OnCollisionExit += OnBulletCollisionExit;
        }

        private void OnBulletCollisionEnter(Bullet bullet, Collision2D collision)
        {
            BulletUtils.TryApplyDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }
        
        private void OnBulletCollisionExit(Bullet bullet, Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("LevelBound"))
                RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            bullet.OnCollisionEntered -= this.OnBulletCollisionEnter;
            bullet.OnCollisionExit -= this.OnBulletCollisionExit;
            _bulletPool.ReturnToPool(bullet);
            _activeBullets.Remove(bullet);
        }
    }
}