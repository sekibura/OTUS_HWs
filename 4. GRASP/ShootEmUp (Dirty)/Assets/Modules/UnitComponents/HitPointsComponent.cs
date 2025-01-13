using System;
using UnityEngine;

namespace ShootEmUp.Modules.Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public Action<GameObject> OnDeath;
        
        [SerializeField] 
        private int _hpValue;
        
        private int _defaultHPValue;

        private void Start()
        {
            _defaultHPValue = _hpValue;
        }
        
        public void ResetValue()
        {
            _hpValue = _defaultHPValue;
        }

        public bool IsAlive() 
        {
            return _hpValue > 0;
        }

        public void ApplyDamage(int damage)
        {
            _hpValue = _hpValue - damage < 0 ? 0 : _hpValue - damage;
            if (_hpValue <= 0)
            {
                OnDeath?.Invoke(gameObject);
            }
        }
    }
}