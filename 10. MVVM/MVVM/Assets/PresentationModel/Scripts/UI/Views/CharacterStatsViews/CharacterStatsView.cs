using System.Collections.Generic;
using OTUSHW.MVVM.UI.Data;
using OTUSHW.MVVM.UI.ViewModel;
using Sekibura.Modules.Base;
using UnityEngine;
using Zenject;

namespace OTUSHW.MVVM.UI.View
{
    public sealed class CharacterStatsView : MonoBehaviour
    {
        [SerializeField]
        private Transform _characterStatsContainer;
        
        private ICharacterStatsModel _characterStatsModel;
        
        [Inject]
        private IObjectPool<CharacterStatView> _characterStatViewPool;

        private readonly List<CharacterStatView> _activeItems = new();

        public void Show(ICharacterStatsModel characterStatsModel)
        {
            _characterStatsModel = characterStatsModel;
            if (_characterStatsModel != null)
            {
                Subscribe();
                UpdateCharacterStatRender();
            }
        }

        private void Subscribe()
        {
            _characterStatsModel.OnCharacterStatChanged += UpdateStatsView;
            _characterStatsModel.OnCharacterStatAdded += UpdateStatsView;
            _characterStatsModel.OnCharacterStatRemoved += UpdateStatsView;
        }

        private void Unsubscribe()
        {
            _characterStatsModel.OnCharacterStatChanged -= UpdateStatsView;
            _characterStatsModel.OnCharacterStatAdded -= UpdateStatsView;
            _characterStatsModel.OnCharacterStatRemoved -= UpdateStatsView;
        }

        private void UpdateStatsView(CharacterStat characterStat)
        {
            UpdateCharacterStatRender();
        }

        private void UpdateCharacterStatRender()
        {
            while (_activeItems.Count > 0)
            {
                _characterStatViewPool.ReturnToPool(_activeItems[0]);
                _activeItems.Remove(_activeItems[0]);
            }

            var stats = _characterStatsModel.CharacterStats;
            foreach (var stat in stats)
            {
                var statItem = _characterStatViewPool.Get();
                statItem.Show(stat);
                statItem.transform.SetParent(_characterStatsContainer);
                statItem.transform.localScale = Vector3.one;
                _activeItems.Add(statItem);
            }
        }

        private void OnDisable()
        {
            if( _characterStatsModel != null)
                Unsubscribe();
        }
    }
}