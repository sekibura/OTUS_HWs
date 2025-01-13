using System;
using ShootEmUp.Modules.GameStateMachine;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public  Action<Bullet, Collision2D> OnCollisionEntered;
        public  Action<Bullet, Collider2D> OnCollisionExit;
        
        [NonSerialized] 
        public int Damage;

        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private Vector2 _defaultVelocity;
        
        [Inject]
        private GameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayStateEnter, onExit: OnGamePlayStateExit);
        }

        private void OnDestroy()
        {
            _gameStateMachine.RemoveListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayStateEnter, onExit: OnGamePlayStateExit);
        }

        private void OnGamePlayStateEnter()
        {
            rigidbody2D.velocity = _defaultVelocity;
        }
        
        private void OnGamePlayStateExit()
        {
            rigidbody2D.velocity = Vector2.zero;
        }
        
        public void InitBullet(Vector2 position, Vector2 velocity, BulletConfig config)
        {
            transform.position= position;
            rigidbody2D.velocity = velocity;
            _defaultVelocity = velocity;
            gameObject.layer = (int)config.PhysicsLayer;
            spriteRenderer.color = config.Color;
            Damage = config.Damage;
        }

        public int GetPhysicsLayer()
        {
            return gameObject.layer;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            OnCollisionExit?.Invoke(this, other);
        }
    }
}