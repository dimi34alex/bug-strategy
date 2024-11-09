using System;
using System.Collections.Generic;
using System.Linq;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.ConfigsRepository
{
    [CreateAssetMenu(fileName = "ConfigsRepository", menuName = "Configs/ConfigsRepository")]
    public class ConfigsRepository : ScriptableObject
    {
        [SerializeField] private List<ScriptableObject> _configs;

        private readonly Type _scriptableObjectType = typeof(ScriptableObject);

        private static ConfigsRepository _instance;
        private Dictionary<Type, ISingleConfig> _configsDictionary;

        public IReadOnlyCollection<ISingleConfig> Configs => _configsDictionary.Values;

        private void OnEnable()
        {
            _instance = this;
            Refresh();
        }

        private void Refresh()
        {
            if (_configs is null)
                return;

            _configsDictionary = new Dictionary<Type, ISingleConfig>(_configs.Count);

            foreach (ScriptableObject config in _configs)
            {
                if (!(config is ISingleConfig))
                    continue;

                Type type = config.GetType();

                if (type == _scriptableObjectType)
                    continue;

                if (!_configsDictionary.ContainsKey(type))
                    _configsDictionary.Add(type, (ISingleConfig)config);
                else
                    Debug.LogWarning($"Duplicate [{type}][{config}]");
            }
        }


#if UNITY_EDITOR

        [UnityEditor.InitializeOnLoadMethod]
        private static void OnRecompile() => RefreshConfigurationList();

        private static void RefreshConfigurationList()
        {
            ScriptableObject[] configurations = Resources.FindObjectsOfTypeAll<ScriptableObject>();

            IEnumerable<ConfigsRepository> configsRepositories = configurations
                .Where(config => config is ConfigsRepository)
                .Select(config => (ConfigsRepository)(config)); 

            IEnumerable<ScriptableObject> configs = configurations.Where(config => config.CastPossible<ISingleConfig>());

            foreach (ConfigsRepository configsRepository in configsRepositories) 
            {
                UnityEditor.EditorUtility.SetDirty(configsRepository);
                configsRepository._configs.Clear();

                foreach (ScriptableObject configuration in configs)
                    configsRepository._configs.Add(configuration);
            }
        }

#endif

        public static TConfig FindConfig<TConfig>() where TConfig : ISingleConfig
        {
            if (_instance._configsDictionary is null || _instance._configs.Count != _instance._configsDictionary.Count)
                _instance.Refresh();

            if (_instance._configsDictionary.TryGetValue(typeof(TConfig), out ISingleConfig config))
                return (TConfig)config;

            return default;
        }
    }
}
