public class UnitRecruitingInformationBase
{
    public bool Empty { get; protected set; }
    public float RecruitinTime { get; protected set; }
    public int StackSize { get; protected set; }
    public float CurrentTime { get; protected set; }
    public int SpawnedBees { get; protected set; }

    public UnitRecruitingInformationBase()
    {
        Empty = true;
        RecruitinTime = 0;
        StackSize = 0;
        CurrentTime = 0;
        SpawnedBees = 0;
    }
    public UnitRecruitingInformationBase(UnitStackBase stack)
    {
        Empty = stack.Empty;
        RecruitinTime = stack.RecruitinTime;
        StackSize = stack.StackSize;
        CurrentTime = stack.CurrentTime;
        SpawnedBees = stack.SpawnedBees;
    }
}
