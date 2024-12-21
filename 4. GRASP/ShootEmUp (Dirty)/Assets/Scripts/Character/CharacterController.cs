using System;
using ShootEmUp.Modules.Input;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private InputManager _inputManager;
        [SerializeField]
        private MoveComponent _moveComponent;
        
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private void OnEnable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
            _inputManager.OnSpacePressed += SpacePressed;
            _inputManager.OnHorizontalMovement += OnHorizontalInput;
        }

        private void OnDisable()
        {
            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
            _inputManager.OnSpacePressed -= SpacePressed;
            _inputManager.OnHorizontalMovement -= OnHorizontalInput;
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        public void OnHorizontalInput(float value)
        {
            _moveComponent.MoveByRigidbodyVelocityHorizontaly(value);
        }
        
        public void SpacePressed()
        {
            OnFlyBullet();
        }
        
        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}