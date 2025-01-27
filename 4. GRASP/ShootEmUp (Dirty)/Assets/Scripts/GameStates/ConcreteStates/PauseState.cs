using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class PauseState : BaseGameState
    {
        public override void Enter()
        {
            Debug.Log("[GameState] PauseState State Enter");
            ViewManager.Show<PauseMenuView>();
        }
    }
}
