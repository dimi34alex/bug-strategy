public class UnitPathData
{
    public readonly IUnitTarget Target;
    public readonly UnitTargetType TargetType;
    public readonly UnitPathType PathType;

    public UnitPathData(IUnitTarget target, UnitTargetType targetType, UnitPathType pathType)
    {
        Target = target;
        TargetType = targetType;
        PathType = pathType;
    }
}
