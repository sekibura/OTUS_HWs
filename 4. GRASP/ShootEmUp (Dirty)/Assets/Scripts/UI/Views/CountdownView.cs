
using Sekibura.ViewSystem;
using ShootEmUp.Modules.GameStateMachine;
using TMPro;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class CountdownView : View
    {
        [SerializeField]
        private TMP_Text _countdownText;

        public override void Show(object parameter = null)
        {
            base.Show(parameter);
            _countdownText.text = " ";
        }

        public void ShowTimeCountdown(float seconds)
        {
             Debug.Log($"Time: {seconds} seconds");
            _countdownText.text = $"Игра начнется через {seconds} секунд";
        }
    }
}
