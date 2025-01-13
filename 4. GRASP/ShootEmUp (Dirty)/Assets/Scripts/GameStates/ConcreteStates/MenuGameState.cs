using System.Collections;
using System.Collections.Generic;
using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class MenuGameState : BaseGameState
    {
        public override void Enter()
        {
            ViewManager.Show<MainMenuView>();
        }
    }
}
