using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Constructions
{
    [CreateAssetMenu(fileName = nameof(ConstructionsConfigsRepository), menuName = "Configs/Constructions/" + nameof(ConstructionsConfigsRepository))]
    public class ConstructionsConfigsRepository : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<ConstructionID, ConstructionBuyCostConfig> data;

        public Cost TakeBuyCost(ConstructionID type) => new Cost(data[type].BuyCost);
        public float GetBuildDuration(ConstructionID type) => data[type].BuildDuration;
    }
}