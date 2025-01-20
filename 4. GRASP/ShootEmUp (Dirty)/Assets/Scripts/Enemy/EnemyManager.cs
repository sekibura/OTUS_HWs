using System;
using System.Collections;
using System.Collections.Generic;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

using ShootEmUp.Modules.Base;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField]
        private EnemyPositions _enemyPositions;
        [Inject]
        private IObjectPool<Enemy> _enemyPool;
        [SerializeField] 
        private BulletConfig _enemyBulletConfig;
        [SerializeField]
        private BulletSystem _bulletSystem;
        [SerializeField]
        private GameObject _targetCharacter;
        [SerializeField] 
        private int _enemyCount = 6;
        

        private List<Enemy> _activeEnemy = new List<Enemy>();
        
        
        [Inject]
        private GameStateMachine _gameStateMachine;
        
        private Coroutine _coroutine;

        private void Start()
        {
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, 
                onEnter: OnGameplayStateEnter,
                onExit: OnGameplayStateExit);
            
            _gameStateMachine.AddListener(GameStatesNames.InitializationStateName, 
                onEnter: OnInitializationStateEnter);
        }
        
        private void OnDestroy()
        {
            _gameStateMachine.RemoveListener(GameStatesNames.GameplayStateName, 
                onEnter:OnGameplayStateEnter, onExit: OnGameplayStateExit );
            
            _gameStateMachine.RemoveListener(GameStatesNames.InitializationStateName, 
                onEnter: OnInitializationStateEnter);
        }

        private void OnGameplayStateEnter()
        {
            StartCoroutineSpawn();
        }
        private void OnGameplayStateExit()
        {
            StopCoroutineSpawn();
        }

        private void OnInitializationStateEnter()
        {
            while (_activeEnemy.Count > 0)
            {
                OnDestroyed(_activeEnemy[0].gameObject);
            }
        }
        
        private void StartCoroutineSpawn()
        {
            _coroutine = StartCoroutine(StartSpawn());
        }
        
        private void StopCoroutineSpawn()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
        
        
        private IEnumerator StartSpawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                
                if(_activeEnemy.Count + 1 > _enemyCount)
                    continue;
                
                Enemy enemy = _enemyPool.Get();
                _activeEnemy.Add(enemy);
                if (enemy != null)
                {
                    enemy.HitPointsComponent.OnDeath += OnDestroyed;
                    enemy.AttackAgent.OnFire += this.OnFire;
                    InitEnemy(enemy);
                }
            }
        }
        
        private void InitEnemy(Enemy enemy)
        {
            Transform spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            Transform attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.HitPointsComponent.ResetValue();
            enemy.MoveAgent.SetDestination(attackPosition.position);
            enemy.AttackAgent.SetTarget(_targetCharacter);
            enemy.AttackAgent.OpenFire();
            Debug.Log("InitEnemy");
        }

        private void OnDestroyed(GameObject enemyGO)
        {
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            if(enemy == null) 
                return;
            
            enemy.AttackAgent.StopFire();
            enemy.HitPointsComponent.OnDeath -= OnDestroyed;
            enemy.AttackAgent.OnFire -= OnFire;
            _enemyPool.ReturnToPool(enemy);
            _activeEnemy.Remove(enemy);
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            _bulletSystem.CreateBullet(
                position: position, 
                velocity:  direction * _enemyBulletConfig.Speed,
                config: _enemyBulletConfig
            );
        }
    }
}