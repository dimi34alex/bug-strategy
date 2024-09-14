using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Constructions.BeeTownHall
{
    [CreateAssetMenu(fileName = nameof(BeeTownHallConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeTownHallConfig))]
    public class BeeTownHallConfig : EvolveConstructionConfigBase<BeeTownHallLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}