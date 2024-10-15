using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BugStrategy.Missions.MissionEditor.EditorConstructions;
using BugStrategy.ResourceSources;
using BugStrategy.Tiles;
using UnityEngine;

namespace BugStrategy.Missions.MissionEditor.Saving
{
    public class MissionSaveAndLoader
    {
#if UNITY_EDITOR
        public static readonly string DirectoryPath = Application.dataPath + "/Source/MissionsSaves";
#else
        public static readonly string DirectoryPath = Application.dataPath + "/CustomMissions";
#endif
        public static IReadOnlyList<string> GetAllMissionsNames()
        {
            if (!Directory.Exists(DirectoryPath)) 
                Directory.CreateDirectory(DirectoryPath);
            
            var info = new DirectoryInfo(DirectoryPath);
            var fileInfo = info.GetFiles();

            var expectedCount = fileInfo.Length;
#if UNITY_EDITOR
            expectedCount /= 2;//Rodion: i do this because in editor we have .meta files
#endif
            
            var editorMissions = new List<string>(expectedCount);
            foreach(var file in fileInfo)
                if (!file.Name.Contains(".meta"))
                    editorMissions.Add(file.Name);

            return editorMissions;
        }
        
        public static void Save(IReadOnlyDictionary<GridKey3, Tile> groundTiles,
            IReadOnlyDictionary<GridKey3, EditorConstruction> constructions,
            IReadOnlyDictionary<GridKey3, ResourceSourceBase> resourceSources)
        {
            var missionSave = new Mission();
            missionSave.SetGroundTiles(groundTiles);
            missionSave.SetConstructions(constructions);
            missionSave.SetResourceSources(resourceSources);
            var json = JsonUtility.ToJson(missionSave);

            var directoryPath = DirectoryPath;
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

        public static void Save(string fileName,
            IReadOnlyDictionary<GridKey3, Tile> groundTiles, 
            IReadOnlyDictionary<GridKey3, EditorConstruction> constructions,
            IReadOnlyDictionary<GridKey3, ResourceSourceBase> resourceSources)
        {
            var missionSave = new Mission();
            missionSave.SetGroundTiles(groundTiles);
            missionSave.SetConstructions(constructions);
            missionSave.SetResourceSources(resourceSources);
            var json = JsonUtility.ToJson(missionSave);

            var directoryPath = DirectoryPath;
            if (!Directory.Exists(directoryPath)) 
                Directory.CreateDirectory(directoryPath);
            
            if (File.Exists($"{directoryPath}/{fileName}.json"))
            {
                var file = File.Create($"{directoryPath}/{fileName}.json");
                file.Close();
            }

            File.WriteAllText($"{directoryPath}/{fileName}.json", json);
        }

        public static async Task Load(CancellationToken cancelToken, string fileName, GroundBuilder groundBuilder, 
            EditorConstructionsBuilder editorConstructionsBuilder, ResourceSourcesBuilder resourceSourceBuilder)
        {
            var directoryPath = DirectoryPath;
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

            var json = await File.ReadAllTextAsync($"{directoryPath}/{fileName}", cancelToken);
            if (cancelToken.IsCancellationRequested) 
                cancelToken.ThrowIfCancellationRequested();
            
            var missionSave = JsonUtility.FromJson<Mission>(json);
            
            groundBuilder.Clear();
            resourceSourceBuilder.Clear();
            editorConstructionsBuilder.Clear();
            
            await groundBuilder.LoadGroundTiles(cancelToken, missionSave.GroundTiles);
            if (cancelToken.IsCancellationRequested) 
                cancelToken.ThrowIfCancellationRequested();
            await editorConstructionsBuilder.LoadGroundTiles(cancelToken, missionSave.Constructions);
            if (cancelToken.IsCancellationRequested) 
                cancelToken.ThrowIfCancellationRequested();
            await resourceSourceBuilder.LoadResourceSources(cancelToken, missionSave.ResourceSources);
            if (cancelToken.IsCancellationRequested) 
                cancelToken.ThrowIfCancellationRequested();
        }
    }
}