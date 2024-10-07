using BugStrategy.ConfigsRepository;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor
{
    [CreateAssetMenu(fileName = nameof(MissionEditorConfig), menuName = "Configs/Missions/Editor/" + nameof(MissionEditorConfig))]
    public class MissionEditorConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public Vector2Int DefaultGridSize { get; private set; } = new(33, 33);
    }
}