using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Constructions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor
{
    [CreateAssetMenu(fileName = nameof(MissionEditorConfig), menuName = "Configs/Missions/Editor/" + nameof(MissionEditorConfig))]
    public class MissionEditorConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private List<Tile> tiles;
        [SerializeField] private List<ConstructionBase> constructions;
        [SerializeField] private List<ResourceSourceBase> resourceSources;

        public IReadOnlyList<Tile> Tiles => tiles;
        public IReadOnlyList<ConstructionBase> Constructions => constructions;
        public IReadOnlyList<ResourceSourceBase> ResourceSources => resourceSources;
    }
}