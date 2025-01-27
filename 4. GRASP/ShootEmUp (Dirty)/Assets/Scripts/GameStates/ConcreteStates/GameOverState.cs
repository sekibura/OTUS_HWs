using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameOverState : BaseGameState
    {
        public override void Enter()
        {
            Debug.Log("[GameState] GameOverState Game State");
            ViewManager.Show<GameOverView>();
        }
    }
}
