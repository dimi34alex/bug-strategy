using System;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public interface ICommand
    {
        public bool IsExecuted { get; }

        public event Action<ICommand> OnExecuted; 
        
        public void Execute();
        public void Undo();
    }
}