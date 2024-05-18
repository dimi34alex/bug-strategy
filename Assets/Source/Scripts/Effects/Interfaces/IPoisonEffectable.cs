namespace Unit.Effects.Interfaces
{
    public interface IPoisonEffectable
    {
        public void TakeDamage(IDamageApplicator damageApplicator, float damageScale);
    }
}