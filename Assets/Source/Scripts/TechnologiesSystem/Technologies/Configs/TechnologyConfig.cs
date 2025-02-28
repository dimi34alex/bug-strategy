using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.TechnologiesSystem.Technologies.Configs
{
    [CreateAssetMenu(fileName = nameof(TechnologyConfig), 
        menuName = "Configs/Technologies/" + nameof(TechnologyConfig))]
    public class TechnologyConfig : ScriptableObject
    {
        [field: SerializeField] public TechnologyId Id { get; private set; }
        [field: SerializeField] public bool IsUnlockedByDefault { get; private set; } = true;
        [field: SerializeField, Min(0), Tooltip("In seconds")] public int ResearchTime { get; private set; }
        [field: Space]
        [field: SerializeField, TextArea(2, 2)] public string TechName { get; private set; }
        [field: Space]
        [field: SerializeField, TextArea(2, 10)] public string UnlockRequirements { get; private set; }
        [field: Space]
        [field: SerializeField, TextArea(2, 10)] public string Description { get; private set; }
        [field: Space]
        [SerializeField] private SerializableDictionary<ResourceID, int> cost;
        
        public Cost TakeCost() 
            => new Cost(cost);
    }
}