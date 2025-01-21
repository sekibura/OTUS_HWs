using System.Collections;
using UnityEngine;

namespace ShootEmUp.Modules.Base
{
    public sealed class CoroutineRunner : MonoBehaviour
    {
        public  Coroutine RunCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
        
        public void StopRunningCoroutine(Coroutine routine)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }
        }
    }
}
