using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;
        public event Action<Bullet, Collider2D> OnCollisionExit;
        
        [NonSerialized] 
        public int Damage;

        [SerializeField]
        private new Rigidbody2D _rigidbody2D;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void InitBullet(Vector2 position, Vector2 velocity, BulletConfig config)
        {
            transform.position= position;
            _rigidbody2D.velocity = velocity;
            gameObject.layer = (int)config.PhysicsLayer;
            _spriteRenderer.color = config.Color;
            Damage = config.Damage;
        }

        public int GetPhysicsLayer()
        {
            return gameObject.layer;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            OnCollisionExit?.Invoke(this, other);
        }
    }
}