using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public class GameOverView: View
    {
        [Inject]
        private GameStateMachine _gameStateMachine;
        
        [SerializeField]
        private Button _playButton;
        
        
        public override void Initialize()
        {
            _playButton.onClick.AddListener(() => _gameStateMachine.SetState(GameStatesNames.InitializationStateName));
        }
    }
}