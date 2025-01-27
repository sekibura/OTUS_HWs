using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        private float _startPositionY;
        [SerializeField]
        private float _endPositionY;
        [SerializeField]
        private float _movingSpeedY;
        [SerializeField]
        private Transform _backgroundTransform;

        private void FixedUpdate()
        {
            if (_backgroundTransform.position.y <= _endPositionY)
            {
                _backgroundTransform.position = new Vector3(
                    gameObject.transform.position.x,
                    _startPositionY,
                    gameObject.transform.position.z
                );
            }

            _backgroundTransform.position -= new Vector3(
                gameObject.transform.position.x,
                _movingSpeedY * Time.fixedDeltaTime,
                gameObject.transform.position.z
            );
        }
    }
}