using System.Collections.Generic;
using UnityEngine;

namespace UnitsRecruitingSystem
{
    public class BeesRecruiting : UnitsRecruiting<BeesRecruitingID>
    {
        public BeesRecruiting(int size, Transform spawnTransform, List<UnitRecruitingData<BeesRecruitingID>> newDatas) :
            base(size, spawnTransform, newDatas) { }
    }
}