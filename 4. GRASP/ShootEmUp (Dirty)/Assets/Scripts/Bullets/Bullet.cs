using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public  Action<Bullet, Collision2D> OnCollisionEntered;
        public  Action<Bullet, Collider2D> OnCollisionExit;
        

        [NonSerialized] public bool isPlayer;
        [NonSerialized] public int damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            OnCollisionExit?.Invoke(this, other);
        }
        
        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
    }
}