using Levels;
using UnityEngine;
using Zenject;

namespace DI
{
    public class CanvasContex : MonoInstaller
    {
        [SerializeField] private ChangeLevelUI _changeLevelUI;
        
        public override void InstallBindings()
        {
            Container.Bind<ChangeLevelUI>().FromInstance(_changeLevelUI).AsSingle();
        }
    }
}