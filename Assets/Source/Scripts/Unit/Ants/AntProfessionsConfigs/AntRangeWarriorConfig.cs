using Projectiles;
using Unit.Professions;
using UnityEngine;

namespace Unit.Ants.ProfessionsConfigs
{
    [CreateAssetMenu(fileName = "AntRangeWarriorConfig", menuName = "Configs/Ant Professions/Range warrior")]
    public class AntRangeWarriorConfig : AntWarriorConfigBase
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }

        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
    }
}