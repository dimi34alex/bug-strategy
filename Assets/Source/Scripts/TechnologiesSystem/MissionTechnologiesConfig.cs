using System.Collections.Generic;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem
{
    [CreateAssetMenu(fileName = nameof(MissionTechnologiesConfig), menuName = "Configs/Mission/" + nameof(MissionTechnologiesConfig))]
    public class MissionTechnologiesConfig : ScriptableObject
    {
        [SerializeField] private List<TechnologyId> technologies;

        public IReadOnlyList<TechnologyId> Technologies => technologies;

        public void CheckDuplicates()
        {
            var checkedIds = new List<TechnologyId>(technologies.Count);
            foreach (var id in technologies)
            {
                if (checkedIds.Contains(id)) 
                    Debug.LogError($"Duplicate of the [{id}] in the [{name}]");
                else
                    checkedIds.Add(id);
            }
        }
    }
}