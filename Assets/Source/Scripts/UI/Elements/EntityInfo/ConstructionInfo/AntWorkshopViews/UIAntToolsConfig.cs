using System.Collections.Generic;
using BugStrategy.Libs;
using BugStrategy.Unit.Ants;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo.AntWorkshopViews
{
    [CreateAssetMenu(fileName = nameof(UIAntToolsConfig), menuName = "Configs/UI/" + nameof(UIAntToolsConfig))]
    public class UIAntToolsConfig : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<ProfessionType, SerializableDictionary<int, Sprite>> toolsIcons;

        public IReadOnlyDictionary<int, Sprite> GetIcons(ProfessionType professionType) 
            => toolsIcons[professionType];
    }
}