using Constructions.LevelSystemCore;
using Unit.Factory;
using UnitsHideCore;
using UnitsRecruitingSystemCore;
using UnityEngine;

namespace Constructions
{
    public class BeeTownHallLevelSystem : ConstructionLevelSystemBase<BeeTownHallLevel>
    {
        private readonly UnitsRecruiter _recruiter;
        private readonly UnitsHider _hider;
        
        public BeeTownHallLevelSystem(ConstructionBase construction, BeeTownHallConfig config, Transform spawnPosition, 
            UnitFactory unitFactory, IResourceGlobalStorage resourceGlobalStorage, ResourceStorage healthStorage, 
            ref UnitsRecruiter recruiter, ref UnitsHider hider) 
            : base(construction, config.Levels,  resourceGlobalStorage, healthStorage)
        {
            _recruiter = recruiter;

            _hider = hider;
        }

        public override void Init(int initialLevelIndex)
        {
            base.Init(initialLevelIndex);
            
            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
        
        protected override void LevelUpLogic()
        {
            base.LevelUpLogic();

            _recruiter.SetStackCount(CurrentLevel.RecruitingSize);
            _recruiter.SetNewDatas(CurrentLevel.BeesRecruitingData);
            _hider.SetCapacity(CurrentLevel.HiderCapacity);
        }
    }
}