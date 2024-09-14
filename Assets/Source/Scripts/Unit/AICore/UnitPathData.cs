namespace BugStrategy.Unit
{
    public class UnitPathData
    {
        public readonly ITarget Target;
        public readonly UnitPathType PathType;

        public TargetType TargetType => Target.TargetType;

        public UnitPathData(ITarget target, UnitPathType pathType)
        {
            Target = target;
            PathType = pathType;
        }
    }
}
