using UnityEngine;


namespace ShootEmUp.Modules.Components
{
    public sealed class TeamComponent : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsPlayer { get; private set; }

        private void Start()
        {
            if(gameObject.layer == LayerMask.NameToLayer("Enemy"))
                IsPlayer = false;
        }
    }
}