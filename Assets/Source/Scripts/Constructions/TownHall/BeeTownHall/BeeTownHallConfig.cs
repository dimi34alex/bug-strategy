using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeeTownHallConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeTownHallConfig))]
    public class BeeTownHallConfig : EvolveConstructionConfigBase<BeeTownHallLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}