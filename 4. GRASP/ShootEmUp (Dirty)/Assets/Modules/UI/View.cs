using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sekibura.ViewSystem
{
    public abstract class View : MonoBehaviour
    {
        public Button BackButton;
        public bool IsEscapeEnable;
        public virtual void Initialize()
        {
            if (BackButton != null)
                BackButton.onClick.AddListener(() =>
                {
                    OnBackButton();
                });
        }
        public virtual void OnBackButton() => ViewManager.ShowLast();
        public virtual void Hide() => gameObject.SetActive(false);
        public virtual void Show(object parameter = null) => gameObject.SetActive(true);

        private void Update()
        {
            if (!IsEscapeEnable)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
                OnBackButton();
        }
    }
}