using MainPlayer;
using Traps.Enum;
using UnityEngine;

namespace Traps
{
    public class ViscousTrap : Trap
    {
        [SerializeField] private Color _activateColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private float _speedModifer;

        private const float DEFAULT_SPEED_MODIFER = 1;

        #region MONO

        private void OnEnable()
        {
            ResetTrap();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var player))
            {
                _meshRenderer.material.color = _activateColor;
                player.SetSpeedModifer(_speedModifer);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerMovement>(out var player))
            {
                player.SetSpeedModifer(DEFAULT_SPEED_MODIFER);
                _meshRenderer.material.color = _defaultColor;
            }
        }

        #endregion


        protected override void ResetTrap()
        {
            _timer = 0f;
            _meshRenderer.material.color = _defaultColor;
            _trapState = DamageTrapState.None;
        }
    }
}