using System.Collections.Generic;
using BugStrategy.Unit;

namespace BugStrategy.ResourcesSystem.ResourcesGlobalStorage
{
    public class TeamsResourcesGlobalStorage : ITeamsResourcesGlobalStorage
    {
        private readonly Dictionary<AffiliationEnum, ResourceRepository> _resourceRepositories;

        public TeamsResourcesGlobalStorage(TeamsResourceGlobalStorageInitialStateConfig initialStateConfig, ResourcesConfig resourceConfigs)
        {
            _resourceRepositories = new Dictionary<AffiliationEnum, ResourceRepository>(initialStateConfig.InitialStates.Count);
            foreach (var initialState in initialStateConfig.InitialStates)
            {
                var resourceRepository = new ResourceRepository(resourceConfigs.ResourceConfigs, initialState.Value);
                _resourceRepositories.Add(initialState.Key, resourceRepository);
            }
        }
    
        public ResourceRepository GetAffiliationResourceRepository(AffiliationEnum affiliation) 
            => _resourceRepositories[affiliation];

        public void ChangeCapacity(AffiliationEnum affiliation, ResourceID resourceID, float capacity) 
            => _resourceRepositories[affiliation].SetCapacity(resourceID, capacity);

        public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value) 
            => _resourceRepositories[affiliation].ChangeValue(resourceID, value);

        public IReadOnlyResource GetResource(AffiliationEnum affiliation, ResourceID resourceID) 
            => _resourceRepositories[affiliation].GetResource(resourceID);
    }
}