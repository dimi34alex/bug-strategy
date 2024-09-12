using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Scripts.UI
{
    [CreateAssetMenu(fileName = nameof(UIUnitConfig), menuName = "Configs/" + nameof(UIUnitConfig))]
    public class UIUnitConfig : ScriptableObject
    {
        [SerializeField] private Sprite _infoSprite;

        [SerializeField] private List<DictionaryCell<UnitTacticsType, Sprite>> _unitTactics;
        [SerializeField] private List<DictionaryCell<ConstructionID, Sprite>> _unitConstruction;
        [SerializeField] private List<DictionaryCell<UnitActionsType, Sprite>> _unitSections;

        public Sprite InfoSprite => _infoSprite;

        public List<DictionaryCell<UnitActionsType, Sprite>> UnitSections => _unitSections;
        public List<DictionaryCell<ConstructionID, Sprite>> UnitConstruction => _unitConstruction;
        public List<DictionaryCell<UnitTacticsType, Sprite>> UnitTactics => _unitTactics;

        public IReadOnlyDictionary<UnitActionsType, Sprite> UnitSectionsDictionary => _unitSections.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    
        public IReadOnlyDictionary<UnitTacticsType, Sprite> UnitTacticsDictionary => _unitTactics.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
        public IReadOnlyDictionary<ConstructionID, Sprite> UnitConstructionDictionary => _unitConstruction.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    }
}
