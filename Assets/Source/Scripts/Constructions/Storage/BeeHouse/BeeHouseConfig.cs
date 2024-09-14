using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Constructions.BeeHouse
{
    [CreateAssetMenu(fileName = nameof(BeeHouseConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeHouseConfig))]
    public class BeeHouseConfig : EvolveConstructionConfigBase<BeeHouseLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}