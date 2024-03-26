namespace Unit
{
    public interface IHealable
    {
        public AffiliationEnum Affiliation { get; }
        public void TakeHeal(float value);
    }
}