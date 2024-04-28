using Constructions.LevelSystemCore;
using UnityEngine;
using Zenject;

namespace Constructions
{
    public class AntAphidFarm : ResourceProduceConstructionBase, IEvolveConstruction
    {
        [SerializeField] private AntAphidFarmConfig config;

        [Inject] private readonly IResourceGlobalStorage _resourceGlobalStorage;
        
        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntAphidFarm;
        public override ResourceProduceCoreBase ResourceProduceCoreBase => _resourceProduceCore;
        public override ResourceProduceConstructionState ProduceConstructionState => _resourceProduceConstructionState;

        public IConstructionLevelSystem LevelSystem { get; private set; }
        
        private ResourceProduceCore _resourceProduceCore;
        private ResourceProduceConstructionState _resourceProduceConstructionState;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            LevelSystem = new AntAphidFarmLevelSystem(this, config, _resourceGlobalStorage, ref _resourceProduceCore, _healthStorage);
            
            _resourceProduceConstructionState = ResourceProduceConstructionState.Paused;

            _updateEvent += OnUpdate;
            Initialized += InitLevelSystem;
        }

        private void InitLevelSystem()
            => LevelSystem.Init(0);

        private void OnUpdate()
        {
            if (_resourceProduceConstructionState != ResourceProduceConstructionState.Proccessing)
                return;
            
            if (!_resourceProduceCore.ProduceResourceIsFull)
                _resourceProduceConstructionState = ResourceProduceConstructionState.Completed;
            
            _resourceProduceCore.Tick(Time.deltaTime);
        }

        public void ExtractResource()
        {
            ResourceBase resource = _resourceGlobalStorage.GetResource(Affiliation, _resourceProduceCore.TargetResourceID);
            var produceResource = _resourceProduceCore.ProducedResource;
            
            if (produceResource.CurrentValue > 0 && resource.CurrentValue < resource.Capacity)
            {
                int extractValue = (int)produceResource.CurrentValue;
                extractValue = (int)Mathf.Clamp(extractValue, 0, (resource.Capacity - resource.CurrentValue));
                int addResource = _resourceProduceCore.ExtractProducedResources(extractValue);
                _resourceGlobalStorage.ChangeValue(Affiliation, _resourceProduceCore.TargetResourceID, addResource);
            }
        }

        //add some worker unit
        public void AddUnit()
        {
            //if units count more 0, then start produce
            //increase produce power
        }

        //remove some worker unit
        public void RemoveUnit()
        {
            //if units count equal 0, then stop produce
            //decrease produce power
        }
    }
}