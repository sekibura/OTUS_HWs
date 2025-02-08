using System;
using System.Collections.Generic;

namespace Lessons.Architecture.PM.UI
{
    public sealed class PopUpManager
    {
        private Dictionary<Type, BasePopup> _popUps = new();
        private BasePopup _currentPopup;
        
        public PopUpManager(List<BasePopup> popUps)
        {
            for (var index = 0; index < popUps.Count; index++)
            {
                if (!_popUps.ContainsKey(popUps[index].GetType()))
                {
                    _popUps.Add(popUps[index].GetType(), popUps[index]);
                }
            }
            HideAll();
        }

        private void HideAll()
        {
            foreach (var VARIABLE in _popUps)
            {
                VARIABLE.Value.Hide();
            }
        }

        public void ShowPopUp<T>(object parameter = null) where T: BasePopup
        {
            if (_popUps.TryGetValue(typeof(T), out var popup))
            {
                if(_currentPopup != null)
                    _currentPopup.Hide();
                
                popup.Show(parameter);
                _currentPopup = popup;
            }
        }
        
        public void Hide<T>() where T: BasePopup
        {
            if (_popUps.TryGetValue(typeof(T), out var popup))
            {
                popup.Hide();
                _currentPopup = null;
            }
        }
    }
}