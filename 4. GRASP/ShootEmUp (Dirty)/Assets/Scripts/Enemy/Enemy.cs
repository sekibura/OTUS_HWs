using System.Collections;
using System.Collections.Generic;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField]
        public EnemyAttackAgent AttackAgent { get; private set; }
        
        [field: SerializeField]
        public HitPointsComponent HitPointsComponent { get; private set; }
        
        [field: SerializeField]
        public EnemyMoveAgent MoveAgent { get; private set; }
        
        [Inject]
        private GameStateMachine _gameStateMachine;
        
        private void Start()
        {
            _gameStateMachine.AddListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayEnter, onExit: OnGamePlayExit, onFixedUpdate: OnFixedUpdate);
        }
        
        private void OnDestroy()
        {
            _gameStateMachine.RemoveListener(GameStatesNames.GameplayStateName, onEnter: OnGamePlayEnter, onExit: OnGamePlayExit, onFixedUpdate: OnFixedUpdate);
        }
        
        private void OnGamePlayEnter()
        {
            if(gameObject.activeSelf)
                AttackAgent.OpenFire();
        }
        
        private void OnGamePlayExit()
        {
            AttackAgent.StopFire();
        }

        private void OnFixedUpdate()
        {
            MoveAgent.OnFixedUpdate();
        }
    }
}
