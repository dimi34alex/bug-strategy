using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.UnitsHideCore;
using UnityEngine;

namespace BugStrategy.Constructions.BeeMercenaryBarrack
{
    public class BeeMercenaryBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeMercenaryBarrackConfig config;
        
        private UnitsHider _hider;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeMercenaryBarrack;
        protected override ConstructionConfigBase ConfigBase => config;
        public IHider Hider => _hider;

        protected override void OnAwake()
        {
            base.OnAwake();

            _recruiter = new UnitsRecruiter(this, 0, unitsSpawnPosition, _unitFactory, TeamsResourcesGlobalStorage);
            _hider = new UnitsHider(this, 0, _unitFactory, unitsSpawnPosition, config.HiderAccess);
            LevelSystem = new BeeMercenaryBarrackLevelSystem(this, _technologyModule, config, TeamsResourcesGlobalStorage, 
                _healthStorage, _recruiter, _hider);
            Initialized += InitLevelSystem;
        }
        
        private void InitLevelSystem()
            => LevelSystem.Init(0);
        
        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }
}