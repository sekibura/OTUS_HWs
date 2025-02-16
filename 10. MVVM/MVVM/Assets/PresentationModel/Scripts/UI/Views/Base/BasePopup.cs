using UnityEngine;

namespace OTUSHW.MVVM.UI.View
{
    public class BasePopup : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}