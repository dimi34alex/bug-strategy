using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeMercenaryBarrackConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeMercenaryBarrackConfig))]
    public class BeeMercenaryBarrackConfig : EvolveConstructionConfigBase<BeeMercenaryBarrackLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}