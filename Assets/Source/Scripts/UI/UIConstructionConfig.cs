using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(UIConstructionConfig), menuName = "Configs/" + nameof(UIConstructionConfig))]
public class UIConstructionConfig : ScriptableObject
{
    [SerializeField] private Sprite _infoSprite;

    [SerializeField] private List<DictionaryCell<ConstructionOperationType, Sprite>> _constructionOperations;
    [SerializeField] private List<DictionaryCell<ConstructionProduct, Sprite>> _constructionProducts;

    public Sprite InfoSprite => _infoSprite;

    public List<DictionaryCell<ConstructionOperationType, Sprite>> ConstructionOperations => _constructionOperations;
    public List<DictionaryCell<ConstructionProduct, Sprite>> ConstructionProducts => _constructionProducts;

    public IReadOnlyDictionary<ConstructionOperationType, Sprite> ConstructionOperationsDictionary => _constructionOperations.ToDictionary(
                                                                            kvp => kvp.Key,
                                                                            kvp => kvp.Value);
    public IReadOnlyDictionary<ConstructionProduct, Sprite> ConstructionProductsDictionary => _constructionProducts.ToDictionary(
                                                                            kvp => kvp.Key,
                                                                            kvp => kvp.Value);
}