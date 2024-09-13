namespace Source.Scripts.ResourcesSystem.ResourcesGlobalStorage
{
    public interface ITeamsResourcesGlobalStorage
    {
        public ResourceRepository GetAffiliationResourceRepository(AffiliationEnum affiliation);

        public void ChangeCapacity(AffiliationEnum affiliation, ResourceID resourceID, float capacity);

        public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value);

        public IReadOnlyResource GetResource(AffiliationEnum affiliation, ResourceID resourceID);
    }
}