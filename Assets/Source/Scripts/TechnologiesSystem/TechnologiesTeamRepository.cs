using System.Collections.Generic;
using BugStrategy.TechnologiesSystem.Technologies;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologiesTeamRepository
    {
        private readonly Dictionary<AffiliationEnum, TechnologiesRepository> _technologiesRepositories = new();
        private readonly TechnologiesFactory _factory;

        public TechnologiesTeamRepository(TechnologiesFactory factory)
        {
            _factory = factory;
        }

        public void HandleUpdate(float deltaTime)
        {
            foreach (var techTeamBlock in _technologiesRepositories.Values) 
                techTeamBlock.HandleUpdate(deltaTime);
        }
        
        public ITechnology GetTechnology(AffiliationEnum affiliation, TechnologyId id)
        {
            if (!_technologiesRepositories.ContainsKey(affiliation)) 
                AddTechnologiesBlock(affiliation);

            return _technologiesRepositories[affiliation].GetTechnology(id);
        }
        
        public T GetTechnology<T>(AffiliationEnum affiliation, TechnologyId id)
            where T: ITechnology
        {
            if (!_technologiesRepositories.ContainsKey(affiliation)) 
                AddTechnologiesBlock(affiliation);

            return _technologiesRepositories[affiliation].GetTechnology<T>(id);
        }

        public void Unlock(AffiliationEnum affiliation, TechnologyId id)
        {
            if (!_technologiesRepositories.ContainsKey(affiliation))
                AddTechnologiesBlock(affiliation);

            _technologiesRepositories[affiliation].Unlock(id);
        }

        private void AddTechnologiesBlock(AffiliationEnum affiliation)
        {
            if (_technologiesRepositories.ContainsKey(affiliation))
            {
                Debug.LogError($"Dictionary already contains tech.block with this affiliation{affiliation}");
                return;
            }
            
            _technologiesRepositories.Add(affiliation, new TechnologiesRepository(_factory));
        }
    }
}