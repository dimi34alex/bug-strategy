using System;

namespace Unit.ProfessionsCore.Processors
{
    public interface IReadOnlyAttackProcessor
    {
        public int EnemiesCount { get; }
        public float AttackRange  { get; }
        public float Damage { get; }
        
        public event Action OnEnterEnemyInZone;
        public event Action OnExitEnemyFromZone;

        public bool CheckAttackDistance(IUnitTarget someTarget);

        public bool CheckEnemiesInAttackZone();
    }
}