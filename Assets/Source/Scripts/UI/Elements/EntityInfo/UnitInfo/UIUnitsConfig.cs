using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.UnitInfo
{
    [CreateAssetMenu(fileName = nameof(UIUnitsConfig), menuName = "Configs/" + nameof(UIUnitsConfig))]
    public class UIUnitsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<UnitType, UIUnitConfig> _unitsUIConfigs;

        public IReadOnlyDictionary<UnitType, UIUnitConfig> UnitsUIConfigs => _unitsUIConfigs;
    }
}
