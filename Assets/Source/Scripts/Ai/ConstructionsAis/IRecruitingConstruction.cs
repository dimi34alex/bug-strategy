using UnitsRecruitingSystemCore;

namespace Source.Scripts.Ai.ConstructionsAis.ConstructionsEvaluators
{
    public interface IRecruitingConstruction
    {
        public AffiliationEnum Affiliation { get; }
        public IReadOnlyUnitsRecruiter Recruiter { get; }

        public void RecruitUnit(UnitType unitType);
    }
}