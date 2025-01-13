using ShootEmUp.Modules.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached => _isReached;

        [SerializeField] 
        private MoveComponent _moveComponent;

        private Vector2 _destination;

        private bool _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnFixedUpdate()
        {
            if (_isReached)
                return;
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}