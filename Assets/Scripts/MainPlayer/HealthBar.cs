using UnityEngine;
using UnityEngine.UI;

namespace MainPlayer
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;

        #region MONO

        private void OnEnable()
        {
            PlayerHealth.ChangeHealth += ChangeHealth;
            PlayerHealth.SetHealth += SetHealth;
        }

        private void OnDisable()
        {
            PlayerHealth.ChangeHealth -= ChangeHealth;
            PlayerHealth.SetHealth -= SetHealth;
        }

        #endregion
        
        private void SetHealth(int maxHealth)
        {
            _healthBar.maxValue = maxHealth;
            _healthBar.value = maxHealth;
        }
        
        private void ChangeHealth(int healthCount)
        {
            _healthBar.value = healthCount;
        }
    }
}