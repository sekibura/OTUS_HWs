using UnityEngine;

namespace Lessons.Architecture.PM.UI
{
    public class BasePopup : MonoBehaviour
    {
        public virtual void Show(object parameter = null)
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}