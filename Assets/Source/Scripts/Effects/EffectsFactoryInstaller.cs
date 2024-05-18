using UnityEngine;
using Zenject;

namespace Unit.Effects
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