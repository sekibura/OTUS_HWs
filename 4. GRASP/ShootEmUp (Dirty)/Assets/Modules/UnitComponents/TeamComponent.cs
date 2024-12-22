using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Modules.Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsPlayer { get; private set; }
    }
}