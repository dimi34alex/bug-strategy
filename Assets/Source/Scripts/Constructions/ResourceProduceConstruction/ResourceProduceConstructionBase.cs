using Constructions.LevelSystemCore;

public abstract class ResourceProduceConstructionBase : EvolvConstruction
{
    public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
    public abstract ResourceProduceConstructionState ProduceConstructionState { get; }
}