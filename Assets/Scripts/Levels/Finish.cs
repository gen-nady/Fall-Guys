using System;
using MainPlayer;
using UnityEngine;

namespace Levels
{
    public class Finish : MonoBehaviour
    {
        public static event Action IsFinished;

        #region MONO

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>())
            {
                IsFinished?.Invoke();
            }
        }

        #endregion
    }
}