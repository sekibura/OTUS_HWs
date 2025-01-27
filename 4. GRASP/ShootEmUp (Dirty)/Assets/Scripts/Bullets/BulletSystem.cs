using ShootEmUp.Modules.Base;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [SerializeField]
        private ObjectPool<Bullet> _bulletPool;
        
        public void CreateBullet(Vector2 position, Vector2 velocity, BulletConfig config)
        {
            var bullet = _bulletPool.Get();
            bullet.InitBullet(position, velocity, config);
            
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
            bullet.OnCollisionEntered -= OnBulletCollisionEnter;
            bullet.OnCollisionExit -= OnBulletCollisionExit;
            _bulletPool.ReturnToPool(bullet);
        }
    }
}