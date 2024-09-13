using System;

public class FrameworkCommander
{
    private static FrameworkCommander _instance;
    private readonly CycleStateMachine _cycleStateMachine;

    private Action<CycleState> _onStateChange;

    public static event Action<CycleState> OnStateChange
    {
        add => _instance._onStateChange += value;
        remove => _instance._onStateChange -= value;
    }

    public FrameworkCommander(CycleStateMachine cycleStateMachine)
    {
        _instance = this;
        _cycleStateMachine = cycleStateMachine;
    }

    public static void SetFrameworkState(CycleState state)
    {
        _instance._cycleStateMachine.SetState(state);
        _instance._onStateChange?.Invoke(state);
    }
}
