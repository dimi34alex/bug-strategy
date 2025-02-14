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
        
        public Technology Create(TechnologyId id) 
        {
            switch (id)
            {
                case TechnologyId.BeeLandmineDamage:
                    return new TechBeeLandmineDamage(_techConfigsProvider.GetConfig<TechBeeLandmineDamageConfig>(id));
                case TechnologyId.HoneyCatapult:
                    return new TechHoneyCatapult();
                case TechnologyId.BumblebeeAccumulation:
                    return new TechBumblebeeAccumulation();
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), id, null);
            }
        }
    }
}