using UnitsHideCore;
using UnityEngine;

namespace Constructions
{
    public class BeeSiegeWeaponsBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeSiegeWeaponsBarrackConfig config;
        
        private UnitsHider _hider;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeSiegeWeaponsBarrack;
        public IHider Hider => _hider;

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeSiegeWeaponsBarrackLevelSystem(config, unitsSpawnPosition, unitFactory,
                ref resourceRepository, ref _healthStorage, ref recruiter, ref _hider);
        }
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(RecruitHoneyCatapult))]
        private void RecruitHoneyCatapult()
            => RecruitUnit(UnitType.HoneyСatapult);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(RecruitMobileHive))]
        private void RecruitMobileHive()
            => RecruitUnit(UnitType.MobileHive);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}