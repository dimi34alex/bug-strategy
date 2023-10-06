using UnityEngine;

[CreateAssetMenu(fileName = "BeesWaxProduceConstructionConfig", menuName = "Config/BeesWaxProduceConstructionConfig")]
public class BeesWaxProduceConstructionConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<BeesWaxProduceConstruction> _configuration;

    public ConstructionConfiguration<BeesWaxProduceConstruction> GetConfiguration()
    {
        return _configuration;
    }
}
