using Projectiles;
using Unit.Ants;
using Unit.Ants.ProfessionsConfigs;

namespace Unit.Professions.Ants
{
    public class AntRangeWarriorProfession : RangeWarriorProfession
    {
        public AntRangeWarriorProfession(AntBase ant, AntRangeWarriorConfig antHandItem, ProjectilesPool projectilesPool) 
            : base(ant, antHandItem.InteractionRange, antHandItem.Cooldown, antHandItem.AttackRange, 
                antHandItem.Damage, antHandItem.ProjectileType, projectilesPool)
        { }
    }
}
