namespace BugStrategy.Unit.RecruitingSystem
{
    public interface IReadOnlyUnitRecruitingStack
    {
        public bool Empty { get; }
        public UnitType UnitId { get; }
        public float RecruitingTime { get; }
        public float RecruitingTimer { get; }
        public int StackSize { get; }
        public int SpawnedUnits { get; }
    }
}