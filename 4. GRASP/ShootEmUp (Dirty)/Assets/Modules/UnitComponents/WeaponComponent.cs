using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Modules.Components
{
    public sealed class WeaponComponent : MonoBehaviour
    {
        public Vector2 Position => _firePoint.position;

        public Quaternion Rotation => _firePoint.rotation;

        [SerializeField]
        private Transform _firePoint;
    }
}