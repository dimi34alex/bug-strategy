using BugStrategy.Constructions.ConstructionLevelSystemCore;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem;
using BugStrategy.Unit.RecruitingSystem;

namespace BugStrategy.Constructions.AntTownHall
{
    public class AntTownHallLevelSystem : ConstructionLevelSystemBase<AntTownHallLevel>
    {
        private readonly UnitsRecruiter _recruiter;

        public AntTownHallLevelSystem(ConstructionBase construction, TechnologyModule technologyModule, AntTownHallConfig config, 
            ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, FloatStorage healthStorage, UnitsRecruiter recruiter) 
            : base(construction, technologyModule, config.Levels,  teamsResourcesGlobalStorage, healthStorage)
        {
            _recruiter = recruiter;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewData(CurrentLevel.RecruitingData);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewData(CurrentLevel.RecruitingData);
        }
    }
}