using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(UIConstructionsConfig), menuName = "Configs/" + nameof(UIConstructionsConfig))]
public class UIConstructionsConfig : ScriptableObject, ISingleConfig
{
    [SerializeField] private SerializableDictionary<ConstructionID, UIConstructionConfig> _constructionsUIConfigs;

    public IReadOnlyDictionary<ConstructionID, UIConstructionConfig> ConstructionsUIConfigs => _constructionsUIConfigs;
}
