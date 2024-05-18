using UnitsHideCore;
using UnitsRecruitingSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class BeeSiegeWeaponsBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeSiegeWeaponsBarrackConfig config;
        
        private UnitsHider _hider;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeSiegeWeaponsBarrack;
        public IHider Hider => _hider;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _recruiter = new UnitsRecruiter(this, 0, unitsSpawnPosition, _unitFactory, _resourceGlobalStorage);
            _hider = new UnitsHider(this, 0, _unitFactory, unitsSpawnPosition, config.HiderAccess);
            LevelSystem = new BeeSiegeWeaponsBarrackLevelSystem(this, config, _resourceGlobalStorage, 
                _healthStorage, _recruiter, _hider);
            InitLevelSystem();
        }
        
        private void InitLevelSystem()
            => LevelSystem.Init(0);
        
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