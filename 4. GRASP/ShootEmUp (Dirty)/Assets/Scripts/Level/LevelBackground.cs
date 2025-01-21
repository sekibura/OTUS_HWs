using System;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

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

        [Inject]
        private GameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, onFixedUpdate : UpdatePosition );
        }

        private void UpdatePosition()
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