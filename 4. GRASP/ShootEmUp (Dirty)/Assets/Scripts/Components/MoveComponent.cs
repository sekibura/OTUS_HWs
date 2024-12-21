using UnityEngine;

namespace ShootEmUp
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private float speed = 5.0f;
        
        public void MoveByRigidbodyVelocityHorizontaly(float value)
        {
            MoveByRigidbodyVelocity(new Vector2(value, rigidbody2D.velocity.y));
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            var nextPosition = this.rigidbody2D.position + vector * this.speed;
            this.rigidbody2D.MovePosition(nextPosition);
        }
    }
}