using System.Collections.Generic;
using UnityEngine;

namespace Constructions
{
    [CreateAssetMenu(fileName = nameof(BeesWaxProduceConfig), menuName = "Configs/Constructions/Main/" + nameof(BeesWaxProduceConfig))]

    public class BeesWaxProduceConfig : EvolveConstructionConfigBase<BeesWaxProduceLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}