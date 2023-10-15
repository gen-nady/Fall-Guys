using Infastructure;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameSettingsContex : MonoInstaller
    {
        [SerializeField] private GameSettings _gameSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        }
    }
}