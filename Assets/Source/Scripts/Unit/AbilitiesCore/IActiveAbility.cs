namespace Source.Scripts.Unit.AbilitiesCore
{
    public interface IActiveAbility
    {
        public AbilityType AbilityType { get; }

        public void TryActivate();
    }
}