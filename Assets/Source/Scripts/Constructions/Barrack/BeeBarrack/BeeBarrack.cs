using UnitsHideCore;
using UnitsRecruitingSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class BeeBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeBarrackConfig config;
        
        private UnitsHider _hider;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeBarrack;
        public IHider Hider => _hider;


        protected override void OnAwake()
        {
            base.OnAwake();

            _recruiter = new UnitsRecruiter(this, 0, unitsSpawnPosition, _unitFactory, _resourceGlobalStorage);
            _hider = new UnitsHider(this, 0, _unitFactory, unitsSpawnPosition, config.HiderAccess);
            LevelSystem = new BeeBarrackLevelSystem(this, config, _resourceGlobalStorage, _healthStorage,
                _recruiter, _hider);
            InitLevelSystem();
        }
        
        private void InitLevelSystem()
            => LevelSystem.Init(0);
        
        //TODO: remove this legacy code, when new ui will be create; use BarrackBase.RecruitUnit(...)
        public void RecruitBees(UnitType beeID)
        {
            int freeStackIndex = _recruiter.FindFreeStack();

            if (freeStackIndex == -1)
            {
                UI_Controller._ErrorCall("All stacks are busy");
                return;
            }

            if (!_recruiter.CheckCosts(beeID))
            {
                UI_Controller._ErrorCall("Need more resources");
                return;
            }

            _recruiter.RecruitUnit(beeID, freeStackIndex);
        }

        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(RecruitHornet))]
        private void RecruitHornet()
            => RecruitUnit(UnitType.Hornet);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(RecruitTruten))]
        private void RecruitTruten()
            => RecruitUnit(UnitType.Truten);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}