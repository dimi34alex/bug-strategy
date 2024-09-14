using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    [CreateAssetMenu(fileName = nameof(AntMeleeWarriorConfig), menuName = "Configs/Units/Ants/" + nameof(AntMeleeWarriorConfig))]
    public class AntMeleeWarriorConfig : AntWarriorConfigBase
    {
        public override ProfessionType ProfessionType => ProfessionType.MeleeWarrior;
    }
}