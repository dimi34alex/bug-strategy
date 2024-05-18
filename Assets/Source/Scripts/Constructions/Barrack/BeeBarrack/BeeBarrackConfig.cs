using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeBarrackConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeBarrackConfig))]
    public class BeeBarrackConfig : EvolveConstructionConfigBase<BeeBarrackLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}