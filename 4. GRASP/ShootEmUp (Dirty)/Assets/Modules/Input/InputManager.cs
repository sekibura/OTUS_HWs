using System;

using UnityEngine;

namespace ShootEmUp.Modules.Input
{
    public sealed class InputManager : MonoBehaviour
    {
        public Action<float> OnHorizontalMovement;
        public Action OnSpacePressed;
        
        public float HorizontalDirection { get; private set; }
        public bool IsSpacePressed { get; private set; }
        

        private void Update()
        {
            ProccessHorizontalInput();
            ProcessSpaceInput();
        }

        private void ProccessHorizontalInput()
        {
            HorizontalDirection = UnityEngine.Input.GetAxis("Horizontal");
            OnHorizontalMovement?.Invoke(HorizontalDirection * Time.fixedDeltaTime);
        }

        private void ProcessSpaceInput()
        {
            bool newValue = UnityEngine.Input.GetKeyDown(KeyCode.Space);

            if (newValue != IsSpacePressed)
            {
                IsSpacePressed = newValue;

                if (!IsSpacePressed)
                    return;
                
                OnSpacePressed?.Invoke();
            }
        }
    }
}
