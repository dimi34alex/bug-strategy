using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using BugStrategy.ResourcesSystem;
using UnityEngine;

namespace BugStrategy.Unit.Pricing
{
    [CreateAssetMenu(fileName = nameof(UnitsRecruitingDataConfig), menuName = "Configs/Units/" + nameof(UnitsRecruitingDataConfig))]
    public class UnitsRecruitingDataConfig : ScriptableObject, ISingleConfig, IUnitsCostsProvider
    {
        [SerializeField] private SerializableDictionary<UnitType, SerializableDictionary<ResourceID, int>> unitsRecruitingData;

        public IReadOnlyDictionary<ResourceID, int> GetUnitRecruitingCost(UnitType unitType) 
            => unitsRecruitingData[unitType];
    }
}