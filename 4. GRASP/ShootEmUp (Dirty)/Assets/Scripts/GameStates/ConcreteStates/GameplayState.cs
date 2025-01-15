using System.Collections;
using System.Collections.Generic;
using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using ShootEmUp.Modules.Input;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class GameplayState : BaseGameState
    {
        [Inject]
        private InputManager _inputManager;
        [Inject]
        private CharacterController _characterController;
        
        public override void Enter()
        {
            Debug.Log("Gameplay Game State");
            ViewManager.Show<GameplayView>();
        }

        public override void Update()
        {
            _inputManager.UpdateInputs();
        }
    }
}
