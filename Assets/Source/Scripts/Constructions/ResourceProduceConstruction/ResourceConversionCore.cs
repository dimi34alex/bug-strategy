using Source.Scripts;
using Source.Scripts.ResourcesSystem;
using UnityEngine;

public class ResourceConversionCore : ResourceProduceCoreBase
{
    private ResourceConversionProccessInfo _resourceConversionProccessInfo;
    private FloatStorage _spendableResouce;
    private float _fractionalProducedResources;
    private float _fractionalSpendableResources;

    protected override ResourceProduceProccessInfoBase _produceProccessInfo => _resourceConversionProccessInfo;

    public ResourceID SpendableResourceID => _resourceConversionProccessInfo.SpendableResourceID;
    public IReadOnlyFloatStorage SpendableResource => _spendableResouce;
    public bool ConversionIsAvailable => MaxPotentialProduceResource >= 1f;
    public float MaxPotentialProduceResource => _fractionalProducedResources + 
        _spendableResouce.CurrentValue * _resourceConversionProccessInfo.SpendRatio;

    public ResourceConversionCore(ResourceConversionProccessInfo conversionProccessInfo) : base(conversionProccessInfo)
    {
        _resourceConversionProccessInfo = conversionProccessInfo;
        _spendableResouce = new FloatStorage(0, conversionProccessInfo.SpendableResourceCapacity);
    }

    protected override int CalculateProducedResourceCount(float time)
    {
        if (!ConversionIsAvailable)
            return 0;

        float targetProduce = time * _resourceConversionProccessInfo.ProducePerSecond;
        float realProduce = Mathf.Min(targetProduce, MaxPotentialProduceResource);
        float realSpend = realProduce / _resourceConversionProccessInfo.SpendRatio;

        _fractionalSpendableResources += realSpend;
        int wholeSpendablePart = (int)_fractionalSpendableResources;
        _fractionalSpendableResources -= wholeSpendablePart;
        _spendableResouce.ChangeValue(-wholeSpendablePart);

        _fractionalProducedResources += realProduce;
        int wholePart = (int)_fractionalProducedResources;
        _fractionalProducedResources -= wholePart;
        return wholePart;
    }

    public void AddSpendableResource(int count)
    {
        _spendableResouce.ChangeValue(count);
    }

    protected override void SetResourceProduceProccessInfoCallback(ResourceProduceProccessInfoBase produceProccessInfo)
    {
        _resourceConversionProccessInfo = produceProccessInfo.Cast<ResourceConversionProccessInfo>();
        SetSpendableResouceProccessInfo(_resourceConversionProccessInfo);
    }

    protected void SetSpendableResouceProccessInfo(ResourceConversionProccessInfo resourceConversionProccessInfo)
    {
        _spendableResouce.SetCapacity(resourceConversionProccessInfo.SpendableResourceCapacity);
    }
}
