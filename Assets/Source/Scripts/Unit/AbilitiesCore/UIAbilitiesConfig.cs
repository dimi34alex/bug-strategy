using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Libs;
using UnityEngine;

namespace BugStrategy.Unit.AbilitiesCore
{
    [CreateAssetMenu(fileName = nameof(UIAbilitiesConfig), menuName = "Configs/UI/" + nameof(UIAbilitiesConfig))]
    public class UIAbilitiesConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private SerializableDictionary<AbilityType, Sprite> abilitiesUiIcons;

        public IReadOnlyDictionary<AbilityType, Sprite> AbilitiesUiIcons => abilitiesUiIcons;
    }
}