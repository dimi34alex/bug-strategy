using System.Collections.Generic;

namespace BugStrategy.ResourcesSystem.ResourcesGlobalStorage
{
    public interface ITeamsResourcesGlobalStorage
    {
        public ResourceRepository GetAffiliationResourceRepository(AffiliationEnum affiliation);

        public void ChangeCapacity(AffiliationEnum affiliation, ResourceID resourceID, float capacity);

        public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value);

        public IReadOnlyResource GetResource(AffiliationEnum affiliation, ResourceID resourceID);

        public bool CanBuy(AffiliationEnum affiliation, Cost cost, int scale = 1);
        public bool CanBuy(AffiliationEnum affiliation, IReadOnlyDictionary<ResourceID, int> cost, int scale = 1);

        public void ChangeValues(AffiliationEnum affiliation, Cost cost, int scale = 1);
        public void ChangeValues(AffiliationEnum affiliation, IReadOnlyDictionary<ResourceID, int> cost, int scale = 1);
    }
}