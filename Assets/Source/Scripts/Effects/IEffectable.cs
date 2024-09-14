namespace BugStrategy.Effects
{
    public interface IEffectable
    {
        AffiliationEnum Affiliation { get; }

        public EffectsProcessor EffectsProcessor { get; }
    }
}