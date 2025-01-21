using System;
using System.Collections;
using ShootEmUp.Modules.Components;
using ShootEmUp.Modules.GameStateMachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUp
{
    public sealed class EnemyAttackAgent : MonoBehaviour
    {
        public Action<GameObject, Vector2, Vector2> OnFire;
        
        [SerializeField] 
        private WeaponComponent _weaponComponent;
        [SerializeField] 
        private EnemyMoveAgent _moveAgent;
        [SerializeField] 
        private float _countdown;

        private GameObject _target;
        private HitPointsComponent _targetHitPointsComponent;
        private float _currentTime;
        private Coroutine _fireCoroutine;
        
        public void SetTarget(GameObject target)
        {
            _target = target;
            _targetHitPointsComponent = _target.GetComponent<HitPointsComponent>();
        }

        public void OpenFire()
        {
            if(_fireCoroutine == null)
                _fireCoroutine = StartCoroutine(FireCoroutine());
        }
        
        public void StopFire()
        {
            if (_fireCoroutine != null)
            {
                StopCoroutine(_fireCoroutine);
                _fireCoroutine = null;
            }
        }
        
        private IEnumerator FireCoroutine()
        {
            while (true)
            {
                if (_moveAgent.IsReached && _targetHitPointsComponent.IsAlive())
                {
                    Fire();
                }
                yield return new WaitForSeconds(_countdown);
            }
        }

        private void Fire()
        {
            var startPosition = _weaponComponent.Position;
            var vector = (Vector2) _target.transform.position - startPosition;
            var direction = vector.normalized;
            OnFire?.Invoke(gameObject, startPosition, direction);
        }
    }
}