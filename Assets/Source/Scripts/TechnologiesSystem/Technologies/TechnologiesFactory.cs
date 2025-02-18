using System;
using BugStrategy.TechnologiesSystem.Technologies.Configs;

namespace BugStrategy.TechnologiesSystem.Technologies
{
    public class TechnologiesFactory
    {
        private readonly TechnologiesConfigsProvider _techConfigsProvider;
        
        public TechnologiesFactory(TechnologiesConfigsProvider techConfigsProvider)
        {
            _techConfigsProvider = techConfigsProvider;
        }
        
        public ITechnology Create(TechnologyId id) 
        {
            switch (id)
            {
                case TechnologyId.BeeLandmineDamage:
                    return new TechBeeLandmineDamage(_techConfigsProvider.GetConfig<TechBeeLandmineDamageConfig>(id));
                case TechnologyId.HoneyCatapult:
                    return new TechHoneyCatapult(_techConfigsProvider.GetConfig(id));
                case TechnologyId.BumblebeeAccumulation:
                    return new TechBumblebeeAccumulation(_techConfigsProvider.GetConfig(id));
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}