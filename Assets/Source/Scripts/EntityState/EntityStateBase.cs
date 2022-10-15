public abstract class EntityStateBase
{
    public abstract EntityStateID EntityStateID { get; }
    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void OnUpdate();
}