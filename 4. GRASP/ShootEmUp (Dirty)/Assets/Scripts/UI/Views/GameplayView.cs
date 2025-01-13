using System.Collections;
using System.Collections.Generic;
using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class GameplayView : View
    {
        [Inject]
        private GameStateMachine _gameStateMachine;
        
        [SerializeField] 
        private Button _pauseBtn;

        public override void Initialize()
        {
            _pauseBtn.onClick.AddListener(() => _gameStateMachine.SetState(GameStatesNames.PauseStateName));
        }
    }
}
