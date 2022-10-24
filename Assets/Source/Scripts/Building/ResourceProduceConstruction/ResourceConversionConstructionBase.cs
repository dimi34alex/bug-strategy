
public abstract class ResourceConversionConstructionBase : ResourceProduceConstructionBase
{
    public abstract ResourceConversionCore ResourceConversionCore { get; }
    public override ResourceProduceCoreBase ResourceProduceCoreBase => ResourceConversionCore;
}
