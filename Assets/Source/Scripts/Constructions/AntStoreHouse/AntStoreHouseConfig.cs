using UnityEngine;

[CreateAssetMenu(fileName = "AntStoreHouseConfig", menuName = "Config/AntStoreHouseConfig")]
public class AntStoreHouseConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<AntStoreHouse> _configuration;

    public ConstructionConfiguration<AntStoreHouse> GetConfiguration()
    {
        return _configuration;
    }
}