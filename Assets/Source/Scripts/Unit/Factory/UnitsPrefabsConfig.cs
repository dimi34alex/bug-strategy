using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Unit.Factory
{
    [CreateAssetMenu(fileName = nameof(UnitsPrefabsConfig), menuName = "Configs/Units/" + nameof(UnitsPrefabsConfig))]
    public class UnitsPrefabsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<UnitType, UnitBase> data;

        public IReadOnlyDictionary<UnitType, UnitBase> Data => data;
    }
}