using System.Collections;
using System.Collections.Generic;
using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CountdownState : BaseGameState
    {
        private float _enterTime;
        private float _deltaTime = 3f;
        private float _nextUpdate;
        private CountdownView _countdownView;

        public override void Enter()
        {
            _countdownView = ViewManager.GetView<CountdownView>();
            _enterTime = Time.time;
            _nextUpdate = Mathf.Floor(Time.time) + 1;
            
            ViewManager.Show<CountdownView>();
            Debug.Log("Обратный отсчет начат.");
        }

        // логика отсчета помещена сюда по причине того, что не хотел в UI реализовывать логику
        public override void Update()
        {
            if (Time.time > _enterTime + _deltaTime)
            {
                Debug.Log("Переход в состояние Gameplay.");
                StateMachine.SetState(GameStatesNames.GameplayStateName);
                return;
            }
            
            float remainingTime = Mathf.Ceil(_enterTime + _deltaTime - Time.time);
            
            if (Time.time >= _nextUpdate)
            {
                // Debug.Log($"Осталось: {remainingTime} секунд");
                _countdownView.ShowTimeCountdown(remainingTime);
                _nextUpdate = Mathf.Floor(Time.time) + 1;
            }
        }
    }
}
