using System;
using BugStrategy.Missions.InGameMissionEditor.Commands;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.Missions.InGameMissionEditor.UI
{
    [RequireComponent(typeof(Button))]
    public class CommandsRedoUiHandler : MonoBehaviour
    {
        [Inject] private CommandsRepository _commandsRepository;

        private Button _button;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(RedoCommand);

            _commandsRepository.OnChange += UpdateView;
            UpdateView();
        }

        private void RedoCommand()
        {
            _commandsRepository.RedoCommand();
            UpdateView();
        }

        private void UpdateView() 
            => gameObject.SetActive(_commandsRepository.UndoCommandsCount > 0);
    }
}