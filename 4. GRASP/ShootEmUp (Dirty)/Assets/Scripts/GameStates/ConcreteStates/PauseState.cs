using System.Collections;
using System.Collections.Generic;
using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using ShootEmUp.Modules.Input;
using UnityEngine;
using Zenject;

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
