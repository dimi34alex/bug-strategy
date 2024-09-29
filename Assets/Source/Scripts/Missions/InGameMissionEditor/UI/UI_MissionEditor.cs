using System;
using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using CycleFramework.Screen;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    public class UI_MissionEditor : UIScreen
    {
        [SerializeField] private ButtonsInitializer<int> tiles;
        [SerializeField] private ButtonsInitializer<ConstructionID> constructions;
        [SerializeField] private ButtonsInitializer<int> resourceSources;

        [Inject] private EditorConstructionsFactory _editorConstructionsFactory;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;
        
        public event Action<int> OnTilePressed;
        public event Action<ConstructionID> OnConstructionPressed;
        public event Action<int> OnResourceSourcePressed;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            tiles.Initialize(_tilesFactory.GetKeys());
            tiles.OnPressed += BuildTile;
            
            constructions.Initialize(_editorConstructionsFactory.GetKeys());
            constructions.OnPressed += BuildConstruction;
            
            resourceSources.Initialize(_resourceSourceFactory.GetKeys());
            resourceSources.OnPressed += BuildResourceSource;
        }
        
        private void BuildTile(int index) 
            => OnTilePressed?.Invoke(index);

        private void BuildConstruction(ConstructionID index) 
            => OnConstructionPressed?.Invoke(index);

        private void BuildResourceSource(int index) 
            => OnResourceSourcePressed?.Invoke(index);
    }
}