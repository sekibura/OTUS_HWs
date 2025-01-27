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

        public override void Enter()
        {
            Debug.Log("[GameState] Gameplay Game State");
            ViewManager.Show<GameplayView>();
        }

        public override void Update()
        {
            // инпут обновляю в состоянии,
            // поскольку не хотел добавлять зависимость в инпут систему на стейт машину
            _inputManager.UpdateInputs();
        }
    }
}
