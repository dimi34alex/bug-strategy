
using BugStrategy.Unit;

namespace BugStrategy
{
    public interface IDamagable
    {
        public AffiliationEnum Affiliation { get; }
        public bool IsAlive { get; }
    
        public void TakeDamage(ITarget attacker, IDamageApplicator damageApplicator, float damageScale = 1);
    }
}