using System;

namespace BugStrategy.EntityState
{
    public abstract class EntityStateBase
    {
        public abstract event Action StateExecuted;
    
        public abstract EntityStateID EntityStateID { get; }
        public abstract void OnStateEnter();
        public abstract void OnStateExit();
        public abstract void OnUpdate();
    }
}