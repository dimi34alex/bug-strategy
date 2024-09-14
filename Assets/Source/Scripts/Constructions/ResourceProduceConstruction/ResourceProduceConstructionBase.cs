using BugStrategy.Constructions.ResourceProduceConstruction.BeesWaxProduceConstruction;

namespace BugStrategy.Constructions.ResourceProduceConstruction
{
    public abstract class ResourceProduceConstructionBase : ConstructionBase
    {
        public abstract ResourceProduceCoreBase ResourceProduceCoreBase { get; }
        public abstract ResourceProduceConstructionState ProduceConstructionState { get; }
    }
}