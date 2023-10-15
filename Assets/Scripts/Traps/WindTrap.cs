using System.Collections.Generic;
using Traps.Enum;
using UnityEngine;

namespace Traps
{
    public class WindTrap : Trap
    {
        [SerializeField] private float _force;

        private CharacterController _playerController;
        private readonly List<Vector3> _directions = new List<Vector3>();
        private Vector3 _currentDirection;

        #region MONO

        private void OnEnable()
        {
            _directions.Add(new Vector3(1, 0, 0));
            _directions.Add(new Vector3(-1, 0, 0));
            _directions.Add(new Vector3(0, 0, 1));
            _directions.Add(new Vector3(0, 0, -1));
        }

        private void Update()
        {
            if (_trapState == DamageTrapState.Activated)
            {
                if (_timer >= 2)
                {
                    SetDirection();
                }

                _playerController.Move(_currentDirection * _force * Time.deltaTime);
                _timer += Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CharacterController>(out var playerController))
            {
                if (_trapState != DamageTrapState.Activated)
                {
                    _trapState = DamageTrapState.Activated;
                    _playerController = playerController;
                    SetDirection();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<CharacterController>())
            {
                ResetTrap();
            }
        }

        #endregion

        private void SetDirection()
        {
            _currentDirection = _directions[Random.Range(0, _directions.Count)];
            _timer = 0;
        }

        protected override void ResetTrap()
        {
            _trapState = DamageTrapState.None;
            _timer = 0;
            _playerController = null;
        }
    }
}