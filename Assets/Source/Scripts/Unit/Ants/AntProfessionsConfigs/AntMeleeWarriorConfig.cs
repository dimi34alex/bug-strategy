using Unit.Professions;
using UnityEngine;

namespace Unit.Ants.ProfessionsConfigs
{
    [CreateAssetMenu(fileName = "AntMeleeWarriorConfig", menuName = "Configs/Ant Professions/Melee warrior")]
    public class AntMeleeWarriorConfig : AntWarriorConfigBase
    {
        public override ProfessionType ProfessionType => ProfessionType.MeleeWarrior;
    }
}