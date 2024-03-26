namespace AttackCooldownChangerSystem
{
    public interface IAttackCooldownChangeable
    {
        public AffiliationEnum Affiliation { get; }

        public AttackCooldownChanger AttackCooldownChanger { get; }
    }
}