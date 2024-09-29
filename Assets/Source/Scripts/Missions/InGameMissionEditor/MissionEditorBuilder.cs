using System;
using System.IO;
using System.Threading;
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

        [Inject] private GroundPositionsRepository _groundPositionsRepository;
        [Inject] private EditorConstructionsRepository _editorConstructionsRepository;
        [Inject] private ResourceSourceRepository _resourceSourceRepository;
        
        [Inject] private CommandsRepository _commandsRepository;
        [Inject] private CommandsFactory _commandsFactory;
        
        private UI_MissionEditor _uiMissionEditor;
        
        private GroundBuilder _groundBuilder;
        private EditorConstructionsBuilder _editorConstructionsBuilder;
        private ResourceSourcesBuilder _resourceSourceBuilder;

        private IGridBuilder _activeBuilder;
        
        private void Awake()
        {
            _uiMissionEditor = FindObjectOfType<UI_MissionEditor>(true);

            _editorConstructionsRepository.SetGridBlocker(new IGridRepository[] { _resourceSourceRepository });
            _resourceSourceRepository.SetGridBlocker(new IGridRepository[] { _editorConstructionsRepository });
            
            _groundBuilder = new GroundBuilder(_gridConfig, _groundPositionsRepository, _tilesFactory, _commandsFactory);
            _editorConstructionsBuilder = new EditorConstructionsBuilder(_gridConfig, _editorConstructionsRepository, _editorConstructionsFactory, _commandsFactory);
            _resourceSourceBuilder = new ResourceSourcesBuilder(_gridConfig, _resourceSourceRepository, _resourceSourceFactory, _commandsFactory);
            
            _uiMissionEditor.OnTilePressed += TilePrep;
            _uiMissionEditor.OnConstructionPressed += ConstrPrep;
            _uiMissionEditor.OnResourceSourcePressed += ResourceSourcePrep;

            _groundBuilder.Generate(config.DefaultGridSize);
            _commandsRepository.Clear();
        }

        private void Update() 
            => _activeBuilder?.ManualUpdate();
        
        private void ResourceSourcePrep(int index)
        {
            _activeBuilder = _resourceSourceBuilder;
            _groundBuilder.DeActivate();
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.Activate(index);
        }

        private void TilePrep(int ind)
        {
            _activeBuilder = _groundBuilder;
            _groundBuilder.Activate(ind);
            _editorConstructionsBuilder.DeActivate();
            _resourceSourceBuilder.DeActivate();
        }

        private void ConstrPrep(ConstructionID id)
        {
            _activeBuilder = _editorConstructionsBuilder;
            _groundBuilder.DeActivate();
            _editorConstructionsBuilder.Activate(id);
            _resourceSourceBuilder.DeActivate();
        }
        
        public void Generate(Vector2Int size)
            => _groundBuilder.Generate(size);

        [ContextMenu("SAVE")]
        public void Save()
        {
            var missionSave = new Mission();
            missionSave.SetGroundTiles(_groundPositionsRepository.Tiles);
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
            missionSave.SetGroundTiles(_groundPositionsRepository.Tiles);
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

        private CancellationTokenSource _mapLoadingCancelToken;
        public async void Load(string fileName)
        {
#if UNITY_EDITOR
            var directoryPath = Application.dataPath + "/Source/MissionsSaves";
#else
            var directoryPath = Application.dataPath + "/CustomMissions";
#endif

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Debug.LogError($"Cant find directory, so file doesnt exist: {directoryPath}");
                return;
            }

            if (!fileName.Contains(".json")) 
                fileName += ".json";
            
            if (!File.Exists($"{directoryPath}/{fileName}"))
            {
                Debug.LogError($"File doesnt exist: {directoryPath}/{fileName}");
                return;
            }

            var json = await File.ReadAllTextAsync($"{directoryPath}/{fileName}");
            var missionSave = JsonUtility.FromJson<Mission>(json);
            
            using (_mapLoadingCancelToken = new CancellationTokenSource())
            {
                try
                {
                    _groundBuilder.Clear();
                    _resourceSourceBuilder.Clear();
                    _editorConstructionsBuilder.Clear();

                    await _groundBuilder.LoadGroundTiles(_mapLoadingCancelToken.Token, missionSave.GroundTiles);
                    await _resourceSourceBuilder.LoadResourceSources(_mapLoadingCancelToken.Token, missionSave.ResourceSources);
                }
                catch (OperationCanceledException e)
                {
                    Debug.Log($"Loading of map was canceled: {e}");
                }
                
                _mapLoadingCancelToken.Dispose();
                _mapLoadingCancelToken = null;
            }            
        }

        private void OnDestroy()
        {
            _mapLoadingCancelToken?.Cancel();
        }
    }
}