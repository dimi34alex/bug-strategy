using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(UIRaceConfig), menuName = "Configs/" + nameof(UIRaceConfig))]
public class UIRaceConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private SerializableDictionary<UnitType, UIUnitConfig> _unitsUIConfigs;

    public IReadOnlyDictionary<UnitType, UIUnitConfig> UnitsUIConfigs => _unitsUIConfigs;
}
