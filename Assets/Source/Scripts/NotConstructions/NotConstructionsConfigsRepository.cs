using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.NotConstructions
{
    [CreateAssetMenu(fileName = nameof(NotConstructionsConfigsRepository), menuName = "Configs/NotConstructions/" + nameof(NotConstructionsConfigsRepository))]
    public class NotConstructionsConfigsRepository : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<NotConstructionID, NotConstructionBuyCostConfig> data;

        public Cost TakeBuyCost (NotConstructionID type) => new Cost(data[type].BuyCost);
        public float GetBuildDuration (NotConstructionID type) => data[type].BuildDuration;
    }
}