using ShootEmUp.Modules.Components;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        public bool IsReached => _isReached;

        [SerializeField] 
        private MoveComponent _moveComponent;

        private Vector2 _destination;
        private bool _isReached;
        private float _destinationDelta = 0.25f;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached)
                return;
            
            var vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= _destinationDelta)
            {
                _isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}