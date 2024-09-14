namespace CycleFramework.Bootload
{
    public class CycleStateMachine
    {
        public CycleState CurrentCycleState { get; private set; }

        public CycleStateMachine(CycleState startState)
        {
            CurrentCycleState = startState;
        }

        public void SetState(CycleState cycleState)
        {
            if (CurrentCycleState == cycleState)
                return;

            CurrentCycleState = cycleState;
        }
    }
}
