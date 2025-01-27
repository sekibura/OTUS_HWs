using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class InitializingGameState : BaseGameState
    {
        public override void Enter()
        {
            Debug.Log("[GameState] Initializing Game State");
            StateMachine.SetState(GameStatesNames.MenuStateName);
        }
    }
}
