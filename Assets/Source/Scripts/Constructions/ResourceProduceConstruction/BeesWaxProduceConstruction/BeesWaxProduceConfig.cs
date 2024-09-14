using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction
{
    [CreateAssetMenu(fileName = nameof(BeesWaxProduceConfig), menuName = "Configs/Constructions/Main/" + nameof(BeesWaxProduceConfig))]

    public class BeesWaxProduceConfig : EvolveConstructionConfigBase<BeesWaxProduceLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}