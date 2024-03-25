using Projectiles;
using Unit.Ants.Professions;
using UnityEngine;

namespace Unit.Ants.Configs.Professions
{
    [CreateAssetMenu(fileName = nameof(AntRangeWarriorConfig), menuName = "Configs/Units/Ants/" + nameof(AntRangeWarriorConfig))]
    public class AntRangeWarriorConfig : AntWarriorConfigBase
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }

        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
    }
}