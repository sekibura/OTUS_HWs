using System;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using ShootEmUp.Modules.Input;
using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [Header("Character components")]
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private MoveComponent _moveComponent;
        [SerializeField]
        private WeaponComponent _weaponComponent;
        [SerializeField]
        private HitPointsComponent _hitPointsComponent;
        [SerializeField]
        private GameObject _playerGameObject;
        
        [Header("Weapon system")]
        [SerializeField] 
        private BulletSystem _bulletSystem;
        [SerializeField] 
        private BulletConfig _bulletConfig;
        
        [Inject]
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _playerGameObject.layer = (int)PhysicsLayer.CHARACTER;
        }
        
        private void Start()
        {
            _gameStateMachine.AddListener(GameStatesNames.InitializationStateName, onEnter: ResetData);
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayStateEnter, onExit: OnGamePlayStateExit );
        }

        private void OnDestroy()
        {
            _gameStateMachine.RemoveListener(GameStatesNames.InitializationStateName, onEnter: ResetData);
            _gameStateMachine.RemoveListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayStateEnter, onExit: OnGamePlayStateExit );
        }

        private void OnGamePlayStateEnter()
        {
            _hitPointsComponent.OnDeath += o => _gameStateMachine.SetState(GameStatesNames.GameOverStateName);
            SubscribeOnInput();
        }
        
        private void OnGamePlayStateExit()
        {
            _hitPointsComponent.OnDeath -= o => _gameStateMachine.SetState(GameStatesNames.GameOverStateName);
            UnsubscribeOnInput();
        }
        
        private void SubscribeOnInput()
        {
            _inputManager.OnSpacePressed += SpacePressed;
            _inputManager.OnHorizontalMovement += OnHorizontalInput;
        }
        
        private void UnsubscribeOnInput()
        {
            _inputManager.OnSpacePressed -= SpacePressed;
            _inputManager.OnHorizontalMovement -= OnHorizontalInput;
        }
        
        public void OnHorizontalInput(float value)
        {
            _moveComponent.MoveByRigidbodyVelocityHorizontaly(value);
        }
        
        public void SpacePressed()
        {
            _bulletSystem.CreateBullet
            (
                position: _weaponComponent.Position, 
                velocity: _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed,
                config: _bulletConfig
            );
        }
        
        public void ResetData()
        {
            _hitPointsComponent.ResetValue();
        }
    }
}