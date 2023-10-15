using MainPlayer;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerContex : MonoInstaller
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private HealthBar _healthBar;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle();
            Container.Bind<HealthBar>().FromInstance(_healthBar).AsSingle();
        }
    }
}