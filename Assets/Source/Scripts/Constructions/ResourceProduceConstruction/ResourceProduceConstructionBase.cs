public abstract class ResourceProduceConstructionBase : ConstructionBase
{
    public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
    public abstract ResourceProduceConstructionState ProduceConstructionState { get; }
}