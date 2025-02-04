using BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using UnityEngine;
using Zenject;

namespace BugStrategy.Constructions.ResourceProduceConstruction.AntMine
{
    public class AntMine : ResourceProduceConstructionBase
    {
        [SerializeField] private AntMineConfig config;

        [Inject] private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        
        public override FractionType Fraction => FractionType.Ants;
        public override ConstructionID ConstructionID => ConstructionID.AntMine;
        public override ResourceProduceCoreBase ResourceProduceCoreBase => _resourceProduceCore;
        public override ResourceProduceConstructionState ProduceConstructionState => _resourceProduceConstructionState;
        protected override ConstructionConfigBase ConfigBase => config;

        private ResourceProduceCore _resourceProduceCore;
        private ResourceProduceConstructionState _resourceProduceConstructionState;
        
        protected override void OnAwake()
        {
            base.OnAwake();

            _healthStorage.SetCapacity(config.HealthPoints);
            _healthStorage.SetValue(config.HealthPoints);
            
            _resourceProduceCore = new ResourceProduceCore(config.ResourceProduceProcessInfo);
            _resourceProduceConstructionState = ResourceProduceConstructionState.Paused;

            _updateEvent += OnUpdate;
        }

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
            var resource = _teamsResourcesGlobalStorage.GetResource(Affiliation, _resourceProduceCore.TargetResourceID);
            var produceResource = _resourceProduceCore.ProducedResource;
            
            if (produceResource.CurrentValue > 0 && resource.CurrentValue < resource.Capacity)
            {
                int extractValue = (int)produceResource.CurrentValue;
                extractValue = (int)Mathf.Clamp(extractValue, 0, (resource.Capacity - resource.CurrentValue));
                int addResource = _resourceProduceCore.ExtractProducedResources(extractValue);
                _teamsResourcesGlobalStorage.ChangeValue(Affiliation, _resourceProduceCore.TargetResourceID, addResource);
            }
        }

        //add some worker unit
        public void AddUnit()
        {
            //increase produce power
            //if units count more 0, then start produce
        }

        //remove some worker unit
        public void RemoveUnit()
        {
            //decrease produce power
            //if units count equal 0, then stop produce
        }
    }
}