using System;
using DG.Tweening;
using MainPlayer;
using Traps.Enum;
using UnityEngine;

namespace Traps
{
    public class DamageTrap : Trap
    {
        [SerializeField] private Color _activateColor;
        [SerializeField] private Color _damageColor;
        [SerializeField] private Color _defaultColor;
        [SerializeField] private int _damageCount;
        [SerializeField] private float _timeToDamage;
        [SerializeField] private float _damageTime;
        [SerializeField] private float _reloadTime;

        private Sequence _damageSequence;
        private int _frameCount;
        
        
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
            if (_trapState == DamageTrapState.Activated && other.TryGetComponent<PlayerHealth>(out var player))
            {
                player.TakeDamage(_damageCount);
                DamageState();
            }
            else
            {
                CheckActivate(other);
            }
        }

        private void OnDestroy()
        {
            _damageSequence.Kill();
        }

        #endregion
      
        private void CheckState()
        {
            switch (_trapState)
            {
                case DamageTrapState.Triggered:
                    if (_timer > _timeToDamage)
                    {
                        _trapState = DamageTrapState.Activated;
                    }
                    break;
                case DamageTrapState.Activated:
                    if (_frameCount == 1)
                    {
                        DamageState();
                    }
                    _frameCount++;
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
        
        private void CheckActivate(Collider other)
        {
            if (_trapState == DamageTrapState.None && other.GetComponent<PlayerHealth>())
            {
                _trapState = DamageTrapState.Triggered;
                _meshRenderer.material.DOColor(_activateColor, _timeToDamage);
            }
        }

        private void DamageState()
        {
            _timer = 0f;
            _trapState = DamageTrapState.Reload;
            _damageSequence.Play();
        }
        
        protected override void ResetTrap()
        {
            _damageSequence = DOTween.Sequence();
            _damageSequence = _damageSequence.Append(_meshRenderer.material.DOColor(_damageColor, _damageTime/2))
                .Append(_meshRenderer.material.DOColor(_defaultColor, _damageTime/2));
            _damageSequence.Rewind();
            _timer = 0f;
            _frameCount = 0;
            _meshRenderer.material.color = _defaultColor;
            _trapState = DamageTrapState.None;
        }
    }
}