using System;
using System.Collections.Generic;

namespace BugStrategy.CommandsCore
{
    public class CommandsRepository
    {
        private readonly List<ICommand> _commands = new(32);
        private readonly ICommandsFactory _commandsFactory;

        private readonly Stack<ICommand> _executedCommands = new(32);
        private readonly Stack<ICommand> _undoCommands = new(32);

        public int ExecutedCommandsCount => _executedCommands.Count;
        public int UndoCommandsCount => _undoCommands.Count;

        public Action OnChange;
        
        public CommandsRepository(ICommandsFactory commandsFactory)
        {
            _commandsFactory = commandsFactory;
            _commandsFactory.OnCommandCreated += AddCommand;
        }
        
        public void UndoCommand()
        {
            if (_executedCommands.Count <= 0)
                return;

            var command = _executedCommands.Pop();
            command.Undo();
            _undoCommands.Push(command);
            OnChange?.Invoke();
        }
        
        public void RedoCommand()
        {
            if (_undoCommands.Count <= 0)
                return;
            
            var command = _undoCommands.Pop();
            command.Execute();
            OnChange?.Invoke();
        }

        public void Clear()
        {
            _executedCommands.Clear();
            _undoCommands.Clear();
            OnChange?.Invoke();
        }
        
        private void AddCommand(ICommand command)
        {
            command.OnExecuted += OnCommandExecuted;
            _commands.Add(command);
            _undoCommands.Clear();
            OnChange?.Invoke();
        }

        private void OnCommandExecuted(ICommand command)
        {
            _commands.Remove(command);
            _executedCommands.Push(command);
            OnChange?.Invoke();
        }
    }
}