namespace BugStrategy.Effects
{
    public interface IPoisonEffectable
    {
        public void TakeDamage(IDamageApplicator damageApplicator, float damageScale);
    }
}