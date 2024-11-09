using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog.Shadows
{
    [CreateAssetMenu(fileName = nameof(TilesShadowerConfig), menuName = "Configs/Tiles/" + nameof(TilesShadowerConfig))]
    public class TilesShadowerConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public TileShadow TileShadowPrefab { get; private set; }
    }
}