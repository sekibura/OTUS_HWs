using System.Collections;
using System.Collections.Generic;
using ShootEmUp.Modules.Components;
using UnityEngine;

namespace ShootEmUp
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField]
        public EnemyAttackAgent AttackAgent { get; private set; }
        
        [field: SerializeField]
        public HitPointsComponent HitPointsComponent { get; private set; }
        
        [field: SerializeField]
        public EnemyMoveAgent MoveAgent { get; private set; }
    }
}
