using UnityEngine;
using Zenject;

public class ConfigsInstaller : MonoInstaller
{
    [SerializeField] private ConfigsRepository _configsRepository;

    public override void InstallBindings()
    {
        Container.BindInstance(_configsRepository).AsSingle();

        foreach (ISingleConfig config in _configsRepository.Configs)
            Container.Bind(config.GetType()).FromInstance(config).AsSingle();
    }
}