
public abstract class ResourceConversionConstructionBase<TBuildingLevel> : ResourceProduceConstructionBase<TBuildingLevel>
    where TBuildingLevel : BuildingLevelBase
{
    public abstract ResourceConversionCore ResourceConversionCore { get; }
    public override ResourceProduceCoreBase ResourceProduceCoreBase => ResourceConversionCore;
}
