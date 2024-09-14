using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Constructions.BeeMercenaryBarrack
{
    [CreateAssetMenu(fileName = nameof(BeeMercenaryBarrackConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeMercenaryBarrackConfig))]
    public class BeeMercenaryBarrackConfig : EvolveConstructionConfigBase<BeeMercenaryBarrackLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}