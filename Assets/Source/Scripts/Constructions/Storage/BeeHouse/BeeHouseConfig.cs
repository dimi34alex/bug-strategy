using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeHouseConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeHouseConfig))]
    public class BeeHouseConfig : EvolveConstructionConfigBase<BeeHouseLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}