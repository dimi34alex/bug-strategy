using UnitsHideCore;
using UnityEngine;

namespace Constructions
{
    public class BeeMercenaryBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeMercenaryBarrackConfig config;
        
        private UnitsHider _hider;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeMercenaryBarrack;
        public IHider Hider => _hider;

        protected override void OnAwake()
        {
            base.OnAwake();

            var resourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeMercenaryBarrackLevelSystem(config, unitsSpawnPosition, unitFactory,
                ref resourceRepository, ref _healthStorage, ref recruiter, ref _hider);
        }

        //TODO: remove this temporary code when new ui will be create
        [ContextMenu(nameof(RecruitMurmur))]
        private void RecruitMurmur()
            => RecruitUnit(UnitType.Murmur);
        
        //TODO: remove this temporary code when new ui will be create
        [ContextMenu(nameof(RecruitSawyer))]
        private void RecruitSawyer()
            => RecruitUnit(UnitType.Sawyer);
        
        //TODO: remove this temporary code when new ui will be create
        [ContextMenu(nameof(RecruitHorntail))]
        private void RecruitHorntail()
            => RecruitUnit(UnitType.Horntail);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}