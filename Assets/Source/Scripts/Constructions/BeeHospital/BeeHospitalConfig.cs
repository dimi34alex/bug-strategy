using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Constructions.BeeHospital
{
    [CreateAssetMenu(fileName = nameof(BeeHospitalConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeHospitalConfig))]
    public class BeeHospitalConfig : EvolveConstructionConfigBase<BeeHospitalLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}