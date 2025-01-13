using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ShootEmUp
{
    public sealed class MainMenuView : View
    {
        [Inject]
        private GameStateMachine _gameStateMachine;
        
        [SerializeField]
        private Button _playButton;

        public override void Initialize()
        {
            base.Initialize();
            _playButton.onClick.AddListener(RunCountDown);
        }

        private void RunCountDown()
        {
            _gameStateMachine.SetState(GameStatesNames.CountdownStateName);
        }
    }
}
