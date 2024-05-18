using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeSiegeWeaponsBarrackConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeSiegeWeaponsBarrackConfig))]
    public class BeeSiegeWeaponsBarrackConfig : EvolveConstructionConfigBase<BeeSiegeWeaponsBarrackLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}