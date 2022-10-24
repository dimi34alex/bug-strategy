using UnityEngine;

public class ResourceConversionCore : ResourceProduceCoreBase
{
    private ResourceConversionProccessInfo _resourceConversionProccessInfo;
    private ResourceStorage _spendableResouce;
    private float _fractionalProducedResources;

    protected override ResourceProduceProccessInfoBase _produceProccessInfo => _resourceConversionProccessInfo;

    public ResourceID SpendableResourceID => _resourceConversionProccessInfo.SpendableResourceID;
    public IReadOnlyResourceStorage SpendableResource => _spendableResouce;
    public bool ConversionIsAvailable => MaxPotentialProduceResource >= 1f;
    public float MaxPotentialProduceResource => _fractionalProducedResources + 
        _spendableResouce.CurrentValue * _resourceConversionProccessInfo.SpendRatio;

    public ResourceConversionCore(ResourceConversionProccessInfo conversionProccessInfo) : base(conversionProccessInfo)
    {
        _resourceConversionProccessInfo = conversionProccessInfo;
    }

    protected override int CalculateProducedResourceCount(float time)
    {
        if (!ConversionIsAvailable)
            return 0;

        float targetProduce = time * _resourceConversionProccessInfo.ProducePerSecond;
        float realProduce = Mathf.Min(targetProduce, MaxPotentialProduceResource);
        float spend = realProduce / _resourceConversionProccessInfo.SpendRatio;

        _fractionalProducedResources += realProduce;
        _spendableResouce.ChangeValue(-spend);

        int wholePart = (int)_fractionalProducedResources;

        return wholePart;
    }

    public void AddSpendableResource(int count)
    {
        _spendableResouce.ChangeValue(count);
    }

    protected override void SetResourceProduceProccessInfoCallback(ResourceProduceProccessInfoBase produceProccessInfo)
    {
        _resourceConversionProccessInfo = produceProccessInfo.Cast<ResourceConversionProccessInfo>();
    }
}
