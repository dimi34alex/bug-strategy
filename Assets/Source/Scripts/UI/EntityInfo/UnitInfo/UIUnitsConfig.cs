using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.UI.EntityInfo.UnitInfo
{
    [CreateAssetMenu(fileName = nameof(UIUnitsConfig), menuName = "Configs/" + nameof(UIUnitsConfig))]
    public class UIUnitsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<UnitType, UIUnitConfig> _unitsUIConfigs;

        public IReadOnlyDictionary<UnitType, UIUnitConfig> UnitsUIConfigs => _unitsUIConfigs;
    }
}
