using Projectiles.Factory;
using Unit.Ants.Configs.Professions;
using Unit.ProfessionsCore;

namespace Unit.Ants.Professions
{
    public class AntRangeWarriorProfession : RangeWarriorProfession
    {
        public AntRangeWarriorProfession(AntBase ant, AntRangeWarriorConfig antHandItem, ProjectileFactory projectilesFactory) 
            : base(ant, antHandItem.InteractionRange, antHandItem.Cooldown, antHandItem.AttackRange, 
                antHandItem.Damage, antHandItem.ProjectileType, projectilesFactory)
        { }
    }
}
