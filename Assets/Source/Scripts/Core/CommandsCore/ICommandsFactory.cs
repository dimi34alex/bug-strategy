using System;

namespace BugStrategy.CommandsCore
{
    public interface ICommandsFactory
    {
        public event Action<ICommand> OnCommandCreated;
    }
}