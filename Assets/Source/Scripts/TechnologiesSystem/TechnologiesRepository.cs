using System.Collections.Generic;
using BugStrategy.TechnologiesSystem.Technologies;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologiesRepository
    {
        private readonly Dictionary<AffiliationEnum, TechnologiesTeamBlock> _technologiesTeamBlocks = new();
        private readonly TechnologiesFactory _factory;

        public TechnologiesRepository(TechnologiesFactory factory)
        {
            _factory = factory;
        }

        public T GetTechnology<T>(AffiliationEnum affiliation, TechnologyId id)
            where T: Technology
        {
            if (!_technologiesTeamBlocks.ContainsKey(affiliation)) 
                AddTechnologiesBlock(affiliation);

            return _technologiesTeamBlocks[affiliation].GetTechnology<T>(id);
        }

        private void AddTechnologiesBlock(AffiliationEnum affiliation)
        {
            if (_technologiesTeamBlocks.ContainsKey(affiliation))
            {
                Debug.LogError($"Dictionary already contains tech.block with this affiliation{affiliation}");
                return;
            }
            
            _technologiesTeamBlocks.Add(affiliation, new TechnologiesTeamBlock(_factory));
        }
    }
}