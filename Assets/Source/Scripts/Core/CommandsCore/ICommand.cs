using System;

namespace BugStrategy.CommandsCore
{
    public interface ICommand
    {
        public bool IsExecuted { get; }

        public event Action<ICommand> OnExecuted; 
        
        public void Execute();
        public void Undo();
    }
}