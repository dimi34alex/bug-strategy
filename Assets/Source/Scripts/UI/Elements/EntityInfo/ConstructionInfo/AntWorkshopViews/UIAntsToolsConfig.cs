using System;
using System.Collections.Generic;
using BugStrategy.ConfigsRepository;
using BugStrategy.Unit;
using BugStrategy.Unit.Ants;
using UnityEngine;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo.AntWorkshopViews
{
    [CreateAssetMenu(fileName = nameof(UIAntsToolsConfig), menuName = "Configs/UI/" + nameof(UIAntsToolsConfig))]
    public class UIAntsToolsConfig : ScriptableObject, ISingleConfig
    {
        [SerializeField] private UIAntToolsConfig standardAnt;
        [SerializeField] private UIAntToolsConfig bigAnt;
        [SerializeField] private UIAntToolsConfig flyingAnt;

        public IReadOnlyDictionary<int, Sprite> GetIcons(UnitType unitType, ProfessionType professionType)
        {
            switch (unitType)
            {
                case UnitType.AntStandard:
                    return standardAnt.GetIcons(professionType);
                case UnitType.AntBig:
                    return bigAnt.GetIcons(professionType);
                case UnitType.AntFlying:
                    return flyingAnt.GetIcons(professionType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitType), unitType, null);
            }
        }
    }
}