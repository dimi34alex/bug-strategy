using System.Collections.Generic;

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
            => _resourceRepositories[affiliation].ChangeCapacity(resourceID, capacity);

        public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value) 
            => _resourceRepositories[affiliation].ChangeValue(resourceID, value);

        public IReadOnlyResource GetResource(AffiliationEnum affiliation, ResourceID resourceID) 
            => _resourceRepositories[affiliation].GetResource(resourceID);

        public bool CanBuy(AffiliationEnum affiliation, Cost cost) 
            => _resourceRepositories[affiliation].CanBuy(cost);

        /// <summary>
        /// Before call it, check that repository have enough resources for this (<see cref="CanBuy"/>)
        /// </summary>
        public void SpendResources(AffiliationEnum affiliation, Cost cost)
            => _resourceRepositories[affiliation].SpendResources(cost);
    }
}