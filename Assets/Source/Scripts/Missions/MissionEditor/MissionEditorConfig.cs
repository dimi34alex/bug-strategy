using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Constructions;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor
{
    [CreateAssetMenu(fileName = nameof(MissionEditorConfig), menuName = "Configs/Missions/Editor/" + nameof(MissionEditorConfig))]
    public class MissionEditorConfig : ScriptableObject, ISingleConfig
    {
        [field: SerializeField] public Vector2Int DefaultGridSize { get; private set; } = new(33, 33);
        [SerializeField] public SerializableDictionary<FractionType, List<ConstructionID>> constructions;

        public IReadOnlyDictionary<FractionType, List<ConstructionID>> Constructions => constructions;
    }
}