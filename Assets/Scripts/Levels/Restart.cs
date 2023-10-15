using System;
using MainPlayer;
using UnityEngine;

namespace Levels
{
    public class Restart : MonoBehaviour
    {
        public static event Action IsRestarted;

        #region MONO

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                IsRestarted?.Invoke();
            }
        }

        #endregion
    }
}