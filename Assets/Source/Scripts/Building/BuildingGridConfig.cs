using UnityEngine;

[CreateAssetMenu(fileName = "BuildingGridConfig", menuName = "Config/BuildingGridConfig")]
public class BuildingGridConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private int _gridTileSize;

    public int GridTileSize => _gridTileSize;
}

