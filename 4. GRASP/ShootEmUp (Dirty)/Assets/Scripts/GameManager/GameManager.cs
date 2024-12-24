using System;
using ShootEmUp.Modules.Components;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        [SerializeField]
        private HitPointsComponent _playerHitPointsController;

        private void OnEnable()
        {
            _playerHitPointsController.OnDeath += FinishGame;
        }

        private void OnDisable()
        {
            _playerHitPointsController.OnDeath -= FinishGame;
        }

        public void FinishGame(GameObject go)
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}