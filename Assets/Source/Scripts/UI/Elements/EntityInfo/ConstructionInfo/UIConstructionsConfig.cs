using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Constructions;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    [CreateAssetMenu(fileName = nameof(UIConstructionsConfig), menuName = "Configs/UI/" + nameof(UIConstructionsConfig))]
    public class UIConstructionsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<ConstructionID, UIConstructionConfig> _constructionsUIConfigs;

        public IReadOnlyDictionary<ConstructionID, UIConstructionConfig> ConstructionsUIConfigs => _constructionsUIConfigs;
    }
}
