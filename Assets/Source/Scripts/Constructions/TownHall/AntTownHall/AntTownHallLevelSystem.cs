using Constructions.LevelSystemCore;
using UnitsRecruitingSystemCore;

namespace Constructions
{
    public class AntTownHallLevelSystem : ConstructionLevelSystemBase<AntTownHallLevel>
    {
        private readonly UnitsRecruiter _recruiter;

        public AntTownHallLevelSystem(ConstructionBase construction, AntTownHallConfig config, 
            IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage, UnitsRecruiter recruiter) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            _recruiter = recruiter;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.RecruitingData);
        }

        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.RecruitingData);
        }
    }
}