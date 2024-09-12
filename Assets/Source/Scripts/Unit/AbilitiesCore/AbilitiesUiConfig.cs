using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Unit.AbilitiesCore
{
    [CreateAssetMenu(fileName = nameof(AbilitiesUiConfig), menuName = "Configs/" + nameof(AbilitiesUiConfig))]
    public class AbilitiesUiConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<AbilityType, Sprite> abilitiesUiIcons;

        public IReadOnlyDictionary<AbilityType, Sprite> AbilitiesUiIcons => abilitiesUiIcons;
    }
}