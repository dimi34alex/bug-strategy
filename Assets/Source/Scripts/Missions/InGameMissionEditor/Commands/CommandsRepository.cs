using System.Collections.Generic;

namespace BugStrategy.Missions.InGameMissionEditor.Commands
{
    public class CommandsRepository
    {
        private readonly List<ICommand> _commands = new(32);
        private readonly CommandsFactory _commandsFactory;

        private readonly Stack<ICommand> _executedCommands = new(32);
        private readonly Stack<ICommand> _undoCommands = new(32);
        
        public CommandsRepository(CommandsFactory commandsFactory)
        {
            _commandsFactory = commandsFactory;
            _commandsFactory.OnCommandCreated += AddCommand;
        }
        
        public void UndoLastCommand()
        {
            if (_executedCommands.Count <= 0)
                return;

            var command = _executedCommands.Pop();
            command.Undo();
            _undoCommands.Push(command);
        }
        
        public void ExecuteLastUndoCommand()
        {
            if (_undoCommands.Count <= 0)
                return;
            
            var command = _undoCommands.Pop();
            command.Execute();
        }

        public void Clear()
        {
            _executedCommands.Clear();
            _undoCommands.Clear();
        }
        
        private void AddCommand(ICommand command)
        {
            command.OnExecuted += OnCommandExecuted;
            _commands.Add(command);
            _undoCommands.Clear();
        }

        private void OnCommandExecuted(ICommand command)
        {
            _commands.Remove(command);
            _executedCommands.Push(command);
        }
    }
}