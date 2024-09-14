using System.Collections.Generic;
using System.Linq;
using BugStrategy.Constructions;
using BugStrategy.Libs;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.UnitInfo
{
    [CreateAssetMenu(fileName = nameof(UIUnitConfig), menuName = "Configs/" + nameof(UIUnitConfig))]
    public class UIUnitConfig : ScriptableObject
    {
        [SerializeField] private Sprite _infoSprite;

        [SerializeField] private List<DictionaryCell<UnitActionsType, Sprite>> _actions;
        [SerializeField] private List<DictionaryCell<UnitTacticType, Sprite>> _unitTactics;
        [SerializeField] private List<DictionaryCell<ConstructionID, Sprite>> _unitConstruction;

        public Sprite InfoSprite => _infoSprite;

        public List<DictionaryCell<UnitActionsType, Sprite>> Actions => _actions;
        public List<DictionaryCell<ConstructionID, Sprite>> UnitConstruction => _unitConstruction;
        public List<DictionaryCell<UnitTacticType, Sprite>> UnitTactics => _unitTactics;

        public IReadOnlyDictionary<UnitActionsType, Sprite> UnitSectionsDictionary => _actions.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    
        public IReadOnlyDictionary<UnitTacticType, Sprite> UnitTacticsDictionary => _unitTactics.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
        public IReadOnlyDictionary<ConstructionID, Sprite> UnitConstructionDictionary => _unitConstruction.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    }
}
