using System;
using Infastructure;
using UnityEngine;

namespace MainPlayer
{
    public class PlayerHealth : MonoBehaviour
    { 
        public static event Action IsRestarted;
        public static event Action<int> ChangeHealth;
        public static event Action<int> SetHealth;

        [SerializeField] private int _defaultHealth;

        private int _health;

        #region MONO

        private void OnEnable()
        {
            GameSettings.OnLevelChanged += ResetHealth;
            ResetHealth();
        }

        private void OnDisable()
        {
            GameSettings.OnLevelChanged -= ResetHealth;
        }

        #endregion

        public void TakeDamage(int count)
        {
            _health -= count;
            if (_health < 0)
            {
                _health = 0;
                IsRestarted?.Invoke();
            }
            
            ChangeHealth?.Invoke(_health);
        }

        private void ResetHealth()
        {
            _health = _defaultHealth;
            SetHealth?.Invoke(_health);
            ChangeHealth?.Invoke(_health);
        }
    }
}