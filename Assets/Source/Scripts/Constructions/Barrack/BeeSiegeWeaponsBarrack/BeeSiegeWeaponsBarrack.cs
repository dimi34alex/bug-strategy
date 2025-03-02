using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.UnitsHideCore;
using UnityEngine;

namespace BugStrategy.Constructions.BeeSiegeWeaponsBarrack
{
    public class BeeSiegeWeaponsBarrack : BarrackBase, IHiderConstruction
    {
        [SerializeField] private BeeSiegeWeaponsBarrackConfig config;
        
        private UnitsHider _hider;

        public override FractionType Fraction => FractionType.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeSiegeWeaponsBarrack;
        protected override ConstructionConfigBase ConfigBase => config;
        public IHider Hider => _hider;

        protected override void OnAwake()
        {
            base.OnAwake();
            
            _recruiter = new UnitsRecruiter(this, 0, unitsSpawnPosition, _unitFactory, TeamsResourcesGlobalStorage);
            _hider = new UnitsHider(this, 0, _unitFactory, unitsSpawnPosition, config.HiderAccess);
            LevelSystem = new BeeSiegeWeaponsBarrackLevelSystem(this, _technologyModule, config, TeamsResourcesGlobalStorage, 
                _healthStorage, _recruiter, _hider);
            Initialized += InitLevelSystem;
            
            OnDeactivation += ReleaseUnitsHider;
        }
        
        private void InitLevelSystem()
            => LevelSystem.Init(0);
        
        private void ReleaseUnitsHider(ITarget _) 
            => _hider.ExtractAll();
    }
}