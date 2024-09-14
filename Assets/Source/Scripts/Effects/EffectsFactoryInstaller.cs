using UnityEngine;
using Zenject;

namespace BugStrategy.Effects
{
    public class EffectsFactoryInstaller : MonoInstaller
    {
        [SerializeField] private EffectsFactory effectsFactory;
        
        public override void InstallBindings()
        {
            Container.Bind<EffectsFactory>().FromInstance(effectsFactory).AsSingle();
        }
    }
}