using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.Input;
using UnityEngine;

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
        private GameObject _playerGameObject;
        
        [Header("Weapon system")]
        [SerializeField] 
        private BulletSystem _bulletSystem;
        [SerializeField] 
        private BulletConfig _bulletConfig;

        private void Awake()
        {
            _playerGameObject.layer = (int)PhysicsLayer.CHARACTER;
        }

        private void OnEnable()
        {
            _inputManager.OnSpacePressed += SpacePressed;
            _inputManager.OnHorizontalMovement += OnHorizontalInput;
        }

        private void OnDisable()
        {
            _inputManager.OnSpacePressed -= SpacePressed;
            _inputManager.OnHorizontalMovement -= OnHorizontalInput;
        }

        private void OnHorizontalInput(float value)
        {
            _moveComponent.MoveByRigidbodyVelocityHorizontaly(value);
        }
        
        private void SpacePressed()
        {
            _bulletSystem.CreateBullet
            (
                position: _weaponComponent.Position, 
                velocity: _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed,
                config: _bulletConfig
            );
        }
    }
}