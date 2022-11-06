using UnityEngine;

public class BeesWaxProduceConstruction : ResourceConversionConstructionBase
{
    [SerializeField] private ResourceConversionProccessInfo _resourceConversionProccessInfo;

    private ResourceConversionCore _resourceConversionCore;
    private ResourceProduceConstructionState _produceConstructionState;

    public override ResourceProduceConstructionState ProduceConstructionState => _produceConstructionState;

    public override ConstructionID ConstructionID => ConstructionID.Bees_Wax_Produce_Construction;
    public override ResourceConversionCore ResourceConversionCore => _resourceConversionCore;

    protected override void OnAwake()
    {
        _resourceConversionCore = new ResourceConversionCore(_resourceConversionProccessInfo);  
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
}
