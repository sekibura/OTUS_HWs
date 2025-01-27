using System;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameManager : IInitializable
    {
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        public void Initialize()
        {
            _gameStateMachine.SetState(GameStatesNames.InitializationStateName);
        }
    }
}