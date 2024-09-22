using System;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    [RequireComponent(typeof(Button))]
    public class CommandsUndoUiHandler : MonoBehaviour
    {
        [Inject] private CommandsRepository _commandsRepository;

        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(UndoCommand);

            _commandsRepository.OnChange += UpdateView;
        }

        private void UndoCommand()
        {
            _commandsRepository.UndoCommand();
            UpdateView();
        }

        private void UpdateView() 
            => gameObject.SetActive(_commandsRepository.ExecutedCommandsCount > 0);
    }
}