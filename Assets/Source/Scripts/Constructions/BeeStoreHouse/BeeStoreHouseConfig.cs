using UnityEngine;

[CreateAssetMenu(fileName = "BeeStoreHouseConfig", menuName = "Config/BeeStoreHouseConfig")]
public class BeeStoreHouseConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<BeeStoreHouse> _configuration;

    public ConstructionConfiguration<BeeStoreHouse> GetConfiguration()
    {
        return _configuration;
    }
}