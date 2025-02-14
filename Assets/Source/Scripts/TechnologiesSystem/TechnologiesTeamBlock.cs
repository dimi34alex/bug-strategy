using System;
using System.Collections.Generic;
using BugStrategy.TechnologiesSystem.Technologies;
using CycleFramework.Extensions;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologiesTeamBlock
    {
        private readonly Dictionary<TechnologyId, Technology> _technologies = new();
        private readonly TechnologiesFactory _factory;

        public TechnologiesTeamBlock(TechnologiesFactory factory)
        {
            _factory = factory;
        }

        public T GetTechnology<T>(TechnologyId id)
            where T : Technology
        {
            if (!_technologies.ContainsKey(id))
            {
                var tech = _factory.Create(id);
                if (tech.TryCast<T>(out var techT))
                    _technologies.Add(id, techT);
                else
                    throw new InvalidCastException($"Cant cast {tech.GetType()} to the {typeof(T)}. Id is {id}");
            }
                
            return _technologies[id].Cast<T>();
        }
    }
}