using System;
using System.Collections.Generic;
using BugStrategy.TechnologiesSystem.Technologies;
using CycleFramework.Extensions;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologiesRepository
    {
        private readonly Dictionary<TechnologyId, ITechnology> _technologies = new();
        private readonly TechnologiesFactory _factory;

        public TechnologiesRepository(TechnologiesFactory factory)
        {
            _factory = factory;
        }

        public void HandleUpdate(float deltaTime)
        {
            foreach (var technology in _technologies.Values) 
                technology.HandleUpdate(deltaTime);
        }
        
        public ITechnology GetTechnology(TechnologyId id)
        {
            if (!_technologies.ContainsKey(id)) 
                _technologies.Add(id, _factory.Create(id));

            return _technologies[id];
        }
        
        public T GetTechnology<T>(TechnologyId id)
            where T : ITechnology
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

        public void Unlock(TechnologyId id)
        {
            if (!_technologies.ContainsKey(id)) 
                _technologies.Add(id, _factory.Create(id));

            _technologies[id].Unlock();
        }
    }
}