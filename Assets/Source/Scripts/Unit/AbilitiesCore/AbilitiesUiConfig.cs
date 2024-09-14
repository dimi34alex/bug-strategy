using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Unit.AbilitiesCore
{
    [CreateAssetMenu(fileName = nameof(AbilitiesUiConfig), menuName = "Configs/" + nameof(AbilitiesUiConfig))]
    public class AbilitiesUiConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<AbilityType, Sprite> abilitiesUiIcons;

        public IReadOnlyDictionary<AbilityType, Sprite> AbilitiesUiIcons => abilitiesUiIcons;
    }
}