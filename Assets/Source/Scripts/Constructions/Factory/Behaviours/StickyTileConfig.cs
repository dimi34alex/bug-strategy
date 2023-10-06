using UnityEngine;

[CreateAssetMenu(fileName = "StickyTileConfig", menuName = "Config/StickyTileConfig")]
public class StickyTileConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private ConstructionConfiguration<StickyTile> _configuration;

    public ConstructionConfiguration<StickyTile> GetConfiguration()
    {
        return _configuration;
    }
}


