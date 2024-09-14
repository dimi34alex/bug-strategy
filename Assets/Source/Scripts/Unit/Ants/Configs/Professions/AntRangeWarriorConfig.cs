using BugStrategy.Projectiles;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    [CreateAssetMenu(fileName = nameof(AntRangeWarriorConfig), menuName = "Configs/Units/Ants/" + nameof(AntRangeWarriorConfig))]
    public class AntRangeWarriorConfig : AntWarriorConfigBase
    {
        [field: SerializeField] public ProjectileType ProjectileType { get; private set; }

        public override ProfessionType ProfessionType => ProfessionType.RangeWarrior;
    }
}