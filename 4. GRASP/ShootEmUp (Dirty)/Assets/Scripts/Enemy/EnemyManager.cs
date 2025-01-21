using System;
using System.Collections;
using System.Collections.Generic;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

using ShootEmUp.Modules.Base;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyManager: IInitializable, IDisposable
    {
        private EnemyPositions _enemyPositions;
        private IObjectPool<Enemy> _enemyPool;
        private BulletConfig _enemyBulletConfig;
        private BulletSystem _bulletSystem;
        private GameObject _targetCharacter;
        private int _enemyCount;
        private List<Enemy> _activeEnemy = new List<Enemy>();
        private GameStateMachine _gameStateMachine;
        private Coroutine _coroutine;
        private CoroutineRunner _coroutineRunner;
        
        [Inject]
        public void Construct(EnemyPositions enemyPositions, IObjectPool<Enemy> enemyPool, [Inject(Id = "EnemyBullet")] BulletConfig enemyBulletConfig, BulletSystem bulletSystem, [Inject(Id = "CharacterTarget")] GameObject targetCharacter, GameStateMachine gameStateMachine, CoroutineRunner coroutineRunner, int enemyCount = 6)
        {
            _enemyPositions = enemyPositions;
            _enemyPool = enemyPool;
            _enemyBulletConfig = enemyBulletConfig;
            _bulletSystem = bulletSystem;
            _targetCharacter = targetCharacter;
            _enemyCount = enemyCount;
            _gameStateMachine = gameStateMachine;
            _coroutineRunner = coroutineRunner;
        }
        
        public void Initialize()
        {
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, 
                onEnter: OnGameplayStateEnter,
                onExit: OnGameplayStateExit);
            
            _gameStateMachine.AddListener(GameStatesNames.InitializationStateName, 
                onEnter: OnInitializationStateEnter);
        }

        public void Dispose()
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
            _coroutine = _coroutineRunner.StartCoroutine(StartSpawn());
        }

        private void StopCoroutineSpawn()
        {
            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
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