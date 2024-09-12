using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.UI.EntityInfo
{
    public class ButtonPanelUIView<T> : UIScreen
    {
        private BackButtonProvider _backButton;
        private ActionButtonProvider[] _buttons;
        private List<T> _unitTacticsTypes;

        private List<UnityAction> _buttonActions = new List<UnityAction>();
        private bool _backButtonIsNull;

        public event Action<T> ButtonClicked;
        public event Action BackButtonClicked;

        private void Awake()
        {
            _backButton = gameObject.GetComponentInChildren<BackButtonProvider>(true);
            _buttons = gameObject.GetComponentsInChildren<ActionButtonProvider>(true);
            _backButtonIsNull = _backButton == null;

            if (!_backButtonIsNull)
                _backButton.Button.onClick.AddListener(() => BackButtonClicked?.Invoke());
        
            for (int i =0; i < _buttons.Length; i++)
            {
                int iValue = i; //Андрей: убираю "ссылочность" i, не трогать
                //Rodion: если интересно что за ссылочность гуглите "замыкание", это вроде оно

                UnityAction action = () => { OnClicked(iValue); };

                _buttonActions.Add(action);
            }

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].Button.onClick.AddListener(_buttonActions[i]);
            }
        }

        private void OnClicked(int numberButton)
        {
            T type = _unitTacticsTypes[numberButton];
            ButtonClicked?.Invoke(type);
        }

        public virtual void SetButtons(bool showBackButton, IReadOnlyDictionary<T, Sprite> images, List<T> orderedTypes)
        {
            _unitTacticsTypes = orderedTypes;

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].gameObject.SetActive(false || i < orderedTypes.Count);

                if (i < orderedTypes.Count)
                    _buttons[i].GetComponent<Image>().sprite = images[orderedTypes[i]];
            }
        
            if (showBackButton && !_backButtonIsNull) 
                _backButton.gameObject.SetActive(true);
        }

        public void TurnOffButtons()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].gameObject.SetActive(false);
            }

            if (!_backButtonIsNull) 
                _backButton.gameObject.SetActive(false);
        }

    }
}