using System;
using Infastructure;
using Traps.Enum;
using UnityEngine;

namespace Traps
{
    public abstract class Trap : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer _meshRenderer;
        [SerializeField] protected BoxCollider _boxCollider;
        
        protected DamageTrapState _trapState;
        protected float _timer;

        private void OnEnable()
        {
            GameSettings.OnLevelChanged += ResetTrap;
        }

        private void OnDisable()
        {
            GameSettings.OnLevelChanged -= ResetTrap;
        }

        protected abstract void ResetTrap();
    }
}