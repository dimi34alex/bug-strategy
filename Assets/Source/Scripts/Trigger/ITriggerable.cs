using System;

namespace BugStrategy.Trigger
{
    public interface ITriggerable
    {
        public event Action<ITriggerable> OnDisableITriggerableEvent;
    }
}
