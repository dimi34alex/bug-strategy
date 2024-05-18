using System.Collections.Generic;
using UnityEngine;

namespace Constructions.BeeHospital
{
    [CreateAssetMenu(fileName = nameof(BeeHospitalConfig), menuName = "Configs/Constructions/Main/" + nameof(BeeHospitalConfig))]
    public class BeeHospitalConfig : EvolveConstructionConfigBase<BeeHospitalLevel>
    {
        [SerializeField] private List<UnitType> hiderAccess;
        
        public IReadOnlyList<UnitType> HiderAccess => hiderAccess;
    }
}