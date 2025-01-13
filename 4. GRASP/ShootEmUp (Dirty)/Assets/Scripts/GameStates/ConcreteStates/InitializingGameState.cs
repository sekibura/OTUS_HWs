using ShootEmUp;
using ShootEmUp.Modules.GameStateMachine;
using ShootEmUp.Modules.Input;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public sealed class InitializingGameState : BaseGameState
    {
        public override void Enter()
        {
            Debug.Log("Initializing Game State");
            StateMachine.SetState(GameStatesNames.MenuStateName);
        }
    }
}
