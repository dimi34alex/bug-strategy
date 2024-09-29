using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Constructions;
using BugStrategy.Factory;
using UnityEngine;

namespace BugStrategy.Missions.InGameMissionEditor.EditorConstructions
{
    [CreateAssetMenu(fileName = nameof(EditorConstructionsConfig), menuName = "Configs/Missions/Editor/" + nameof(EditorConstructionsConfig))]
    public class EditorConstructionsConfig : ScriptableObject, IFactoryConfig<ConstructionID, EditorConstruction>, ISingleConfig
    {
        [SerializeField] private List<EditorConstruction> constructions;

        private Dictionary<ConstructionID, EditorConstruction> _constructions;
        
        public IReadOnlyDictionary<ConstructionID, EditorConstruction> GetData()
        {
            if (_constructions == null)
            {
                _constructions = new Dictionary<ConstructionID, EditorConstruction>(constructions.Count);
                for (int i = 0; i < constructions.Count; i++)
                {
                    var id = constructions[i].constructionID;
                    if (_constructions.ContainsKey(id))
                        Debug.LogWarning($"Duplicate of: {id}");
                    else
                        _constructions.Add(id, constructions[i]);
                }
            }

            return _constructions;
        }
    }
}