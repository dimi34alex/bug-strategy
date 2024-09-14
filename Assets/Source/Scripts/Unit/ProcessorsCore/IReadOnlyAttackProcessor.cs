using System;

namespace BugStrategy.Unit.ProcessorsCore
{
    public interface IReadOnlyAttackProcessor
    {
        public int EnemiesCount { get; }
        public float AttackRange  { get; }
        public float Damage { get; }
        
        public event Action OnEnterEnemyInZone;
        public event Action OnExitEnemyFromZone;

        public bool TargetInZone(IUnitTarget someTarget);
        
        public bool CheckAttackDistance(IUnitTarget someTarget);

        public bool CheckEnemiesInAttackZone();
    }
}