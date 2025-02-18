using System.Collections.Generic;
using CycleFramework.Extensions;

namespace BugStrategy.TechnologiesSystem.Technologies.Configs
{
    public class TechnologiesConfigsProvider
    {
        private readonly Dictionary<TechnologyId, TechnologyConfig> _configs;
        
        public TechnologiesConfigsProvider(TechnologiesConfigRepository repository)
        {
            _configs = new Dictionary<TechnologyId, TechnologyConfig>(repository.Configs.Count);
            foreach (var config in repository.Configs) 
                _configs.Add(config.Id, config);
        }
        
        public TechnologyConfig GetConfig(TechnologyId id) 
            => _configs[id];

        public T GetConfig<T>(TechnologyId id) where T : TechnologyConfig =>
            GetConfig(id).Cast<T>();
    }
}