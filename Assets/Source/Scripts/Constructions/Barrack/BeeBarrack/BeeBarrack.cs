using UnitsHideCore;
using UnityEngine;

namespace Constructions
{
    public class BeeBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeBarrackConfig config;
        
        private UnitsHider _hider;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeBarrack;
        public IHider Hider => _hider;


        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeBarrackLevelSystem(config, unitsSpawnPosition, unitFactory,
                ref resourceRepository, ref _healthStorage, ref recruiter, ref _hider);
        }
        
        //TODO: remove this legacy code, when new ui will be create; use BarrackBase.RecruitUnit(...)
        public void RecruitBees(UnitType beeID)
        {
            int freeStackIndex = recruiter.FindFreeStack();

            if (freeStackIndex == -1)
            {
                UI_Controller._ErrorCall("All stacks are busy");
                return;
            }

            if (!recruiter.CheckCosts(beeID))
            {
                UI_Controller._ErrorCall("Need more resources");
                return;
            }

            recruiter.RecruitUnit(beeID, freeStackIndex);
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