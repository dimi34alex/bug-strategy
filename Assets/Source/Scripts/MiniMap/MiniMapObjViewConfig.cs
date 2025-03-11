using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.MiniMap
{
    [CreateAssetMenu(fileName = nameof(MiniMapObjViewConfig), menuName = "Configs/MiniMap/" + nameof(MiniMapObjViewConfig))]
    public class MiniMapObjViewConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public MiniMapObjView MiniMapObjViewPrefab { get; private set; }
        [field: Header("Icons")]
        [field: SerializeField] public Sprite UnitIcon { get; private set; }
        [field: SerializeField] public Sprite ConstructionIcon { get; private set; }
        [field: SerializeField] public Sprite ResourceSourceIcon { get; private set; }
        [field: Header("Colors")]
        [field: SerializeField] public Color FriendlyColor { get; private set; } = Color.white;
        [field: SerializeField] public Color EnemyColor { get; private set; } = Color.white;
        [field: SerializeField] public Color NeutralColor { get; private set; } = Color.white;
    }
}