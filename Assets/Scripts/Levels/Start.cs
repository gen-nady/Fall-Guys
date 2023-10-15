using System;
using MainPlayer;
using UnityEngine;

namespace Levels
{
    public class Start : MonoBehaviour
    {
        public static event Action IsStarted;
        
        #region MONO
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                IsStarted?.Invoke();
            }
        }

        #endregion
    }
}