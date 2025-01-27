using System;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using ShootEmUp.Modules.Input;
using UnityEngine;
using Zenject;


namespace ShootEmUp
{
    public sealed class CharacterController : IInitializable, IDisposable
    {
        private MoveComponent _moveComponent;
        private WeaponComponent _weaponComponent;
        private HitPointsComponent _hitPointsComponent;
        
        private GameObject _playerGameObject;
        private InputManager _inputManager;
        private BulletSystem _bulletSystem;
        private BulletConfig _bulletConfig;
        private GameStateMachine _gameStateMachine;

        [Inject]
        public CharacterController(MoveComponent moveComponent, 
            WeaponComponent weaponComponent, 
            HitPointsComponent hitPointsComponent,
            
            GameStateMachine stateMachine,
            [Inject(Id = "CharacterBullet")] BulletConfig bulletConfig,
            BulletSystem bulletSystem,
            InputManager inputManager,
            [Inject(Id = "PlayerGameObject")] GameObject playerGameObject
            )
        {
            _moveComponent = moveComponent;
            _weaponComponent = weaponComponent;
            _hitPointsComponent = hitPointsComponent;
            
            _gameStateMachine = stateMachine;
            _bulletConfig = bulletConfig;
            _bulletSystem = bulletSystem;
            _inputManager = inputManager;
            _playerGameObject = playerGameObject;
        }

        public void Initialize()
        {
            _playerGameObject.layer = (int)PhysicsLayer.CHARACTER;
            _gameStateMachine.AddListener(GameStatesNames.InitializationStateName, onEnter: ResetData);
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayStateEnter, onExit: OnGamePlayStateExit );
        }

        public void Dispose()
        {
            _gameStateMachine.RemoveListener(GameStatesNames.InitializationStateName, onEnter: ResetData);
            _gameStateMachine.RemoveListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayStateEnter, onExit: OnGamePlayStateExit );
        }

        private void OnGamePlayStateEnter()
        {
            _hitPointsComponent.OnDeath += OnPlayerDeath;
            SubscribeOnInput();
        }

        private void OnGamePlayStateExit()
        {
            _hitPointsComponent.OnDeath -= OnPlayerDeath;
            UnsubscribeOnInput();
        }

        private void OnPlayerDeath(GameObject character)
        {
            _gameStateMachine.SetState(GameStatesNames.GameOverStateName);
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