using System;

public class FrameworkCommander
{
    private static FrameworkCommander _instance;
    private readonly CycleStateMachine _cycleStateMachine;
    private readonly GlobalData _globalData;

    private Action<CycleState> _onStateChange;

    public static GlobalData GlobalData => _instance._globalData;

    public static event Action<CycleState> OnStateChange
    {
        add => _instance._onStateChange += value;
        remove => _instance._onStateChange -= value;
    }

    public FrameworkCommander(CycleStateMachine cycleStateMachine)
    {
        _instance = this;
        _cycleStateMachine = cycleStateMachine;
        _globalData = SerializeExtensions.Deserialize<GlobalData>() ?? new GlobalData();
    }

    public static void SetFrameworkState(CycleState state)
    {
        _instance._cycleStateMachine.SetState(state);
        _instance._onStateChange?.Invoke(state);
    }

    public static void Save()
    {
        SerializeExtensions.Serialize(_instance._globalData);
    }
}
