public interface IResourceGlobalStorage
{
    public ResourceRepository GetAffiliationResourceRepository(AffiliationEnum affiliation);

    public void ChangeCapacity(AffiliationEnum affiliation, ResourceID resourceID, float capacity);

    public void ChangeValue(AffiliationEnum affiliation, ResourceID resourceID, float value);

    public ResourceBase GetResource(AffiliationEnum affiliation, ResourceID resourceID);
}