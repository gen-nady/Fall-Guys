using System;
using MainPlayer;
using Traps.Enum;
using UnityEngine;

namespace Traps
{
    public class VanishTrap : Trap
    {
        [SerializeField] private float _timeToVanish;
        [SerializeField] private float _vanishTime;
        [SerializeField] private float _reloadTime;
        
        #region MONO

        private void OnEnable()
        {
            ResetTrap();
        }

        private void Update()
        {
            if (_trapState != DamageTrapState.None)
            {
                CheckState();
                _timer += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckActivate(other);
        }

        private void OnTriggerStay(Collider other)
        {
            CheckActivate(other);
        }
        
        #endregion
      
        private void CheckState()
        {
            switch (_trapState)
            {
                case DamageTrapState.Triggered:
                    if (_timer > _timeToVanish)
                    {
                        _trapState = DamageTrapState.Activated;
                        ActivatedTrap();
                        _timer = 0f;
                    }
                    break;
                case DamageTrapState.Activated:
                    if (_timer > _vanishTime)
                    {
                        _trapState = DamageTrapState.Reload;
                        DeactivatedTrap();
                        _timer = 0f;
                    }
                    break;
                case DamageTrapState.Reload:
                    if (_timer > _reloadTime)
                    {
                        ResetTrap();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ActivatedTrap()
        {
            _meshRenderer.enabled = false;
            _boxCollider.enabled = false;
        } 
        
        private void DeactivatedTrap()
        {
            _meshRenderer.enabled = true;
            _boxCollider.enabled = true;
        }
        
        private void CheckActivate(Collider other)
        {
            if (_trapState == DamageTrapState.None && other.GetComponent<PlayerHealth>())
            {
                _trapState = DamageTrapState.Triggered;
            }
        }
        
        protected override void ResetTrap()
        {
            _timer = 0f;
            DeactivatedTrap();
            _trapState = DamageTrapState.None;
        }
    }
}