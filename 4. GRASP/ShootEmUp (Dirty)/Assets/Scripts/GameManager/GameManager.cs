using System;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private HitPointsComponent _playerHitPointsController;

        // private void OnEnable()
        // {
        //     _playerHitPointsController.OnDeath += FinishGame;
        // }
        //
        // private void OnDisable()
        // {
        //     _playerHitPointsController.OnDeath -= FinishGame;
        // }

        public void FinishGame(GameObject go)
        {
            Debug.Log("Game over!");
            //Time.timeScale = 0;
            _gameStateMachine.SetState(GameStatesNames.GameOverStateName);
        }




        #region  NewShit
        
        private GameStateMachine _gameStateMachine;
        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void Start()
        {
             _gameStateMachine.SetState(GameStatesNames.InitializationStateName);
        }
        #endregion  
    }
}