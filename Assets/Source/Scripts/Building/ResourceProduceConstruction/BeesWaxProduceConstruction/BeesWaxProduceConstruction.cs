using UnityEngine;

public class BeesWaxProduceConstruction : ResourceConversionConstructionBase<BeesWaxProduceLevel>
{
    private ResourceConversionCore _resourceConversionCore;
    private ResourceProduceConstructionState _produceConstructionState;

    public override ResourceProduceConstructionState ProduceConstructionState => _produceConstructionState;

    public override ConstructionID ConstructionID => ConstructionID.Bees_Wax_Produce_Construction;
    public override ResourceConversionCore ResourceConversionCore => _resourceConversionCore;

    protected override void OnAwake()
    {
        base.OnAwake();
        
        _resourceConversionCore = new ResourceConversionCore(CurrentLevel.ResourceConversionProccessInfo);
        
        levelSystem = new BeesWaxProduceLevelSystem(levelSystem, HealPoints, _resourceConversionCore);

        _updateEvent += OnUpdate;
    }

    private void OnUpdate()
    {
        if (_produceConstructionState != ResourceProduceConstructionState.Proccessing)
            return;

        if (!_resourceConversionCore.ConversionIsAvailable)
            _produceConstructionState = ResourceProduceConstructionState.Completed;

        _resourceConversionCore.Tick(Time.deltaTime);
    }

    public void SetConversionPauseState(bool paused)
    {
        if (_produceConstructionState is ResourceProduceConstructionState.Completed)
            return;

        _produceConstructionState = paused ? ResourceProduceConstructionState.Paused : ResourceProduceConstructionState.Proccessing;
    }

    public void AddSpendableResource(int addPollen)
    {
        ResourceBase pollen = ResourceGlobalStorage.GetResource(ResourceID.Pollen);
        IReadOnlyResourceStorage spendableResource = _resourceConversionCore.SpendableResource;
        
        if (pollen.CurrentValue > 0 && spendableResource.CurrentValue < spendableResource.Capacity)
        {
            addPollen = (int)Mathf.Clamp(addPollen, 0, pollen.Capacity - pollen.CurrentValue);
            addPollen = (int)Mathf.Clamp(addPollen, 0, spendableResource.Capacity - spendableResource.CurrentValue);
            _resourceConversionCore.AddSpendableResource(addPollen);
            ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, -addPollen);
        }
        
        if (_resourceConversionCore.ConversionIsAvailable)
            _produceConstructionState = ResourceProduceConstructionState.Proccessing;
        
        IReadOnlyResourceStorage produceResource = _resourceConversionCore.ProducedResource;
        if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
            SetConversionPauseState(false);
        else
            SetConversionPauseState(true);
    }
    
    public void ExtractProduceResource()
    {
        ResourceBase beesWax = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax);
        IReadOnlyResourceStorage produceResource = _resourceConversionCore.ProducedResource;

        if (produceResource.CurrentValue > 0 && beesWax.CurrentValue < beesWax.Capacity)
        {
            int extractValue = (int)produceResource.CurrentValue;
            extractValue = (int)Mathf.Clamp(extractValue, 0, (beesWax.Capacity - beesWax.CurrentValue));
            int addBeesWax = _resourceConversionCore.ExtractProducedResources(extractValue);
            ResourceGlobalStorage.ChangeValue(ResourceID.Bees_Wax, addBeesWax); 
        }
        
        if (_resourceConversionCore.ConversionIsAvailable)
            _produceConstructionState = ResourceProduceConstructionState.Proccessing;
        
        IReadOnlyResourceStorage spendableResource = _resourceConversionCore.SpendableResource;
        if (spendableResource.CurrentValue > 0 && produceResource.CurrentValue < produceResource.Capacity)
            SetConversionPauseState(false);
        else
            SetConversionPauseState(true);
    }

    public IReadOnlyResourceStorage TakeSpendableResourceInformation()
    {
        return _resourceConversionCore.SpendableResource;
    }
    
    public IReadOnlyResourceStorage TakeProduceResourceInformation()
    {
        return _resourceConversionCore.ProducedResource;
    }
}
