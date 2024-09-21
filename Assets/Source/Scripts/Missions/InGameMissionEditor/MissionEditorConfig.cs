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
        [field: SerializeField] public Vector2Int DefaultGridSize { get; private set; } = new(33, 33);
        [SerializeField] private List<Tile> tiles;
        [SerializeField] private List<ConstructionBase> constructions;
        [SerializeField] private List<ResourceSourceBase> resourceSources;

        private Dictionary<int, Tile> _tiles = null;
        private Dictionary<int, ConstructionBase> _constructions = null;
        private Dictionary<int, ResourceSourceBase> _resourceSources = null;
        
        /// <summary>
        /// Result cached
        /// </summary>
        public IReadOnlyDictionary<int, Tile> GetTiles()
        {
            if (_tiles != null)
                return _tiles;

            _tiles = new Dictionary<int, Tile>(tiles.Count);
            for (int i = 0; i < tiles.Count; i++) 
                _tiles.Add(i, tiles[i]);

            return _tiles;
        }
        
        /// <summary>
        /// Result cached
        /// </summary>
        public IReadOnlyDictionary<int, ConstructionBase> GetConstructions()
        {
            if (_constructions != null)
                return _constructions;
            
            _constructions = new Dictionary<int, ConstructionBase>(constructions.Count);
            for (int i = 0; i < constructions.Count; i++) 
                _constructions.Add(i, constructions[i]);

            return _constructions;
        }
        
        /// <summary>
        /// Result cached
        /// </summary>
        public IReadOnlyDictionary<int, ResourceSourceBase> GetResourceSources()
        {
            if (_resourceSources != null)
                return _resourceSources;
            
            _resourceSources = new Dictionary<int, ResourceSourceBase>(resourceSources.Count);
            for (int i = 0; i < resourceSources.Count; i++) 
                _resourceSources.Add(i, resourceSources[i]);

            return _resourceSources;
        }
    }
}