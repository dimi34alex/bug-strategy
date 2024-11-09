using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog.Shadows
{
    [CreateAssetMenu(fileName = nameof(TilesShadowsConfig), menuName = "Configs/Tiles/" + nameof(TilesShadowsConfig))]
    public class TilesShadowsConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public TileShadow TileShadowPrefab { get; private set; }
    }
}