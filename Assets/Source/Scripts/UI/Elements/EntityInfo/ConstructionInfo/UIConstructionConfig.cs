﻿using System.Collections.Generic;
using System.Linq;
using BugStrategy.Libs;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    [CreateAssetMenu(fileName = nameof(UIConstructionConfig), menuName = "Configs/" + nameof(UIConstructionConfig))]
    public class UIConstructionConfig : ScriptableObject
    {
        [SerializeField] private Sprite _infoSprite;

        [SerializeField] private List<DictionaryCell<ConstructionActionsType, Sprite>> _constructionActions;
        [SerializeField] private List<DictionaryCell<UnitType, Sprite>> recruiting;

        public Sprite InfoSprite => _infoSprite;

        public List<DictionaryCell<ConstructionActionsType, Sprite>> ConstructionActions => _constructionActions;
        public List<DictionaryCell<UnitType, Sprite>> Recruiting => recruiting;

        public IReadOnlyDictionary<ConstructionActionsType, Sprite> ConstructionActionsDictionary => _constructionActions.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    
        public IReadOnlyDictionary<UnitType, Sprite> RecruitingDictionary => recruiting.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value);
    }
}