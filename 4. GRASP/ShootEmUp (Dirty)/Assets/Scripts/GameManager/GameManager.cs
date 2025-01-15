using System;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
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
    }
}