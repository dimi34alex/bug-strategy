using BugStrategy.Unit;

namespace BugStrategy.Constructions.UnitsRecruitingSystem
{
    public interface IRecruitingConstruction
    {
        public AffiliationEnum Affiliation { get; }
        public IReadOnlyUnitsRecruiter Recruiter { get; }

        public void RecruitUnit(UnitType unitType);
        public void CancelRecruit(int stackIndex);
    }
}