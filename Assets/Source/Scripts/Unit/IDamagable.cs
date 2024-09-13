
using System;

public interface IDamagable
{
    public AffiliationEnum Affiliation { get; }
    public bool IsAlive { get; }
    
    public void TakeDamage(IUnitTarget attacker, IDamageApplicator damageApplicator, float damageScale = 1);
}