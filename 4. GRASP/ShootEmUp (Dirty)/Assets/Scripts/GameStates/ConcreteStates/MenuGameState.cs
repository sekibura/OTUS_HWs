using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MenuGameState : BaseGameState
    {
        public override void Enter()
        {
            Debug.Log("[GameState] MenuGameState Game State");
            ViewManager.Show<MainMenuView>();
        }
    }
}
