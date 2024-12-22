using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Modules.Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        [SerializeField]
        private float _speed = 5.0f;
        
        public void MoveByRigidbodyVelocityHorizontaly(float value)
        {
            MoveByRigidbodyVelocity(new Vector2(value, _rigidbody2D.velocity.y));
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector2 newPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(newPosition);
        }
    }
}