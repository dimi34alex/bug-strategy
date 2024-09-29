using System.IO;
using System.Linq;
using BugStrategy.Constructions;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using BugStrategy.Missions.InGameMissionEditor.EditorConstructions;
using BugStrategy.Missions.InGameMissionEditor.GridRepositories;
using BugStrategy.Missions.InGameMissionEditor.Saving;
using BugStrategy.Missions.InGameMissionEditor.UI;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor
{
    public class MissionEditorBuilder : MonoBehaviour
    {
        [SerializeField] private MissionEditorConfig config;

        [Inject] private GridConfig _gridConfig;
        [Inject] private TilesFactory _tilesFactory;
        [Inject] private EditorConstructionsFactory _editorConstructionsFactory;
        [Inject] private ResourceSourceFactory _resourceSourceFactory;

        [Inject] private TilesPositionsRepository _tilesPositionsRepository;
        [Inject] private EditorConstructionsRepository _editorConstructionsRepository;
        [Inject] private ResourceSourceRepository _resourceSourceRepository;
        
        [Inject] private CommandsRepository _commandsRepository;
        [Inject] private CommandsFactory _commandsFactory;
        
        private MissionEditorUI _missionEditorUI;
        
        private TilesBuilder _tilesBuilder;
        private EditorConstructionsBuilder _editorConstructionsBuilder;
        private ResourceSourcesBuilder _resourceSourceBuilder;

        private IGridBuilder _activeBuilder;
        
        private void Awake()
        {
            _missionEditorUI = FindObjectOfType<MissionEditorUI>();

            _editorConstructionsRepository.SetGridBlocker(new IGridRepository[] { _resourceSourceRepository });
            _resourceSourceRepository.SetGridBlocker(new IGridRepository[] { _editorConstructionsRepository });
            
            _tilesBuilder = new TilesBuilder(_gridConfig, _tilesPositionsRepository, _tilesFactory, _commandsFactory);
            _editorConstructionsBuilder = new EditorConstructionsBuilder(_gridConfig, _editorConstructionsRepository, _editorConstructionsFactory, _commandsFactory);
            _resourceSourceBuilder = new ResourceSourcesBuilder(_gridConfig, _resourceSourceRepository, _resourceSourceFactory, _commandsFactory);
            
            _missionEditorUI.OnTilePressed += TilePrep;
            _missionEditorUI.OnConstructionPressed += ConstrPrep;
            _missionEditorUI.OnResourceSourcePressed += ResourceSourcePrep;

            _tilesBuilder.Generate(config.DefaultGridSize);
            _commandsRepository.Clear();
        }

        private void Update() 
            => _activeBuilder?.ManualUpdate();
        
        private void ResourceSourcePrep(int index)
        {
            _activeBuilder = _resourceSourceBuilder;
            _tilesBuilder.DeActivate();
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.Activate(index);
        }

        private void TilePrep(int ind)
        {
            _activeBuilder = _tilesBuilder;
            _tilesBuilder.Activate(ind);
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.DeActivate();
        }

        private void ConstrPrep(ConstructionID id)
        {
            _activeBuilder = _editorConstructionsBuilder;
            _tilesBuilder.DeActivate();
            _editorConstructionsBuilder.Activate(id);
            _resourceSourceBuilder.DeActivate();
        }

        [ContextMenu("UndoLastCommand")]
        private void Undo() 
            => _commandsRepository.UndoCommand();

        [ContextMenu("ExecuteLastUndoCommand")]
        private void Ex() 
            => _commandsRepository.RedoCommand();
        
        public void Generate(Vector2Int size)
            => _tilesBuilder.Generate(size);

        [ContextMenu("SAVE")]
        public void Save()
        {
            var missionSave = new Mission();
            missionSave.SetGroundTiles(_tilesPositionsRepository.Tiles);
            missionSave.SetResourceSources(_resourceSourceRepository.Tiles);
            var json = JsonUtility.ToJson(missionSave);

#if UNITY_EDITOR
            var directoryPath = Application.dataPath + "/Source/MissionsSaves";
#else
            var directoryPath = Application.dataPath + "/CustomMissions";
#endif
            const string fileName = "MissionSave";
            
            var index = "";
            int i = 1;
            while (File.Exists($"{directoryPath}/{fileName}{index}.json"))
            {
                index = $"_{i}";
                i++;
            }

            if (!Directory.Exists(directoryPath)) 
                Directory.CreateDirectory(directoryPath);

            var file = File.Create($"{directoryPath}/{fileName}{index}.json");
            file.Close();
            File.WriteAllText($"{directoryPath}/{fileName}{index}.json", json);
        }

        public void Save(string fileName)
        {
            var missionSave = new Mission();
            missionSave.SetGroundTiles(_tilesPositionsRepository.Tiles);
            missionSave.SetResourceSources(_resourceSourceRepository.Tiles);
            var json = JsonUtility.ToJson(missionSave);

#if UNITY_EDITOR
            var directoryPath = Application.dataPath + "/Source/MissionsSaves";
#else
            var directoryPath = Application.dataPath + "/CustomMissions";
#endif
            if (!Directory.Exists(directoryPath)) 
                Directory.CreateDirectory(directoryPath);
            
            if (File.Exists($"{directoryPath}/{fileName}.json"))
            {
                var file = File.Create($"{directoryPath}/{fileName}.json");
                file.Close();
            }

            File.WriteAllText($"{directoryPath}/{fileName}.json", json);
        }
    }
}