using UnityEngine;

[CreateAssetMenu(fileName = "BeeHouseConfig", menuName = "Config/BeeHouseConfig")]
public class BeeHouseConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<BeeHouse> _configuration;

    public ConstructionConfiguration<BeeHouse> GetConfiguration()
    {
        return _configuration;
    }
}
