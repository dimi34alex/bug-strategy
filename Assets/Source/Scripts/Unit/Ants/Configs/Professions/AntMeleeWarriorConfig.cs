using Unit.ProfessionsCore;
using UnityEngine;

namespace Unit.Ants.Configs.Professions
{
    [CreateAssetMenu(fileName = nameof(AntMeleeWarriorConfig), menuName = "Configs/Units/Ants/" + nameof(AntMeleeWarriorConfig))]
    public class AntMeleeWarriorConfig : AntWarriorConfigBase
    {
        public override ProfessionType ProfessionType => ProfessionType.MeleeWarrior;
    }
}