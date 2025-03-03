using UnityEngine;
using Zenject;

namespace BugStrategy.ConfigsRepository
{
    public class ConfigsInstaller : MonoInstaller
    {
        [SerializeField] private ConfigsRepository _configsRepository;

        public override void InstallBindings()
        {
            Container.BindInstance(_configsRepository).AsSingle();

            foreach (ISingleConfig config in _configsRepository.Configs)
                Container.BindInterfacesAndSelfTo(config.GetType()).FromInstance(config).AsSingle();
        }
    }
}