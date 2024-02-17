using Factories;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private FactoryBase[] _factories;
        
        public override void InstallBindings()
        {
            foreach (var factory in _factories)
                Container.Bind(factory.GetType()).FromInstance(factory).AsSingle();
        }
    }
}