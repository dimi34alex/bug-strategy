using Construction.TownHalls;
using Constructions.LevelSystemCore;
using UnitsHideCore;
using UnityEngine;

namespace Constructions
{
    public class BeeTownHall : TownHallBase, IHiderConstruction
    {
        [SerializeField] private BeeTownHallConfig config;

        private UnitsHider _hider;

        public override AffiliationEnum Affiliation => AffiliationEnum.Bees;
        public override ConstructionID ConstructionID => ConstructionID.BeeTownHall;
        public IHider Hider => _hider;
        
        public override IConstructionLevelSystem LevelSystem { get; protected set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            
            gameObject.name = "TownHall";

            var takeResourceRepository = ResourceGlobalStorage.ResourceRepository;
            LevelSystem = new BeeTownHallLevelSystem(config, workerBeesSpawnPosition, _unitFactory, 
                ref takeResourceRepository, ref _healthStorage, ref _recruiter, ref _hider);
        }

        //TODO: remove this temporary code, when new ui will be create
        [ContextMenu(nameof(ExtractHidedUnit))]
        public void ExtractHidedUnit()
            => Hider.ExtractUnit(0);
    }  
}
