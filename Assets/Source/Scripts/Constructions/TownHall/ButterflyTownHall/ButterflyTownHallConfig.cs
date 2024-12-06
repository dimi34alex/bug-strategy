using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Constructions.ButterflyTownHall
{
    [CreateAssetMenu(fileName = nameof(ButterflyTownHallConfig), menuName = "Configs/Constructions/Main/" + nameof(ButterflyTownHallConfig))]
    public class ButterflyTownHallConfig : EvolveConstructionConfigBase<ButterflyTownHallLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}