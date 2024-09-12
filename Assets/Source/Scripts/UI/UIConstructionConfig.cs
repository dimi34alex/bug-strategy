using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Scripts.UI
{
    [CreateAssetMenu(fileName = nameof(UIConstructionConfig), menuName = "Configs/" + nameof(UIConstructionConfig))]
    public class UIConstructionConfig : ScriptableObject
    {
        [SerializeField] private Sprite _infoSprite;

        [SerializeField] private List<DictionaryCell<ConstructionActionsType, Sprite>> _constructionActions;
        [SerializeField] private List<DictionaryCell<UnitType, Sprite>> recruiting;
        [SerializeField] private List<DictionaryCell<ConstructionProduct, Sprite>> _constructionProducts;

        public Sprite InfoSprite => _infoSprite;

        public List<DictionaryCell<ConstructionActionsType, Sprite>> ConstructionActions => _constructionActions;
        public List<DictionaryCell<UnitType, Sprite>> Recruiting => recruiting;
        public List<DictionaryCell<ConstructionProduct, Sprite>> ConstructionProducts => _constructionProducts;

        public IReadOnlyDictionary<ConstructionActionsType, Sprite> ConstructionActionsDictionary => _constructionActions.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    
        public IReadOnlyDictionary<UnitType, Sprite> RecruitingDictionary => recruiting.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    
        public IReadOnlyDictionary<ConstructionProduct, Sprite> ConstructionProductsDictionary => _constructionProducts.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    }
}