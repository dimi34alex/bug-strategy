using UnityEngine;

[CreateAssetMenu(fileName = "BuildingProgressConstructionConfig", menuName = "Config/BuildingProgressConstructionConfig")]
public class BuildingProgressConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<BuildingProgressConstruction> _configuration;

    public ConstructionConfiguration<BuildingProgressConstruction> GetConfiguration()
    {
        return _configuration;
    }
}