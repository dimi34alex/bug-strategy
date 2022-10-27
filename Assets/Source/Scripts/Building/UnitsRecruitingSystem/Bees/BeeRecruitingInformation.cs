public class BeeRecruitingInformation : UnitRecruitingInformationBase
{
    public BeesRecruitingID CurrentID { get; private set; }

    public BeeRecruitingInformation() : base() { }

    public BeeRecruitingInformation(BeesStack stack) : base(stack)
    {
        CurrentID = stack.CurrentID;
    }
}