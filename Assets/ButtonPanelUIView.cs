using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonPanelUIView<T> : UIScreen
{
    private Button[] _buttons;
    private List<T> _unitTacticsTypes;

    private List<UnityAction> _buttonActions = new List<UnityAction>();

    public event Action<T> ButtonClicked;

    private void Awake()
    {
        _buttons = gameObject.GetComponentsInChildren<Button>(true);

        for (int i =0; i < _buttons.Length; i++)
        {
            int iValue = i; // убираю "ссылочность" i, не трогать

            UnityAction action = () => { OnClicked(iValue); };

            _buttonActions.Add(action);
        }

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].onClick.AddListener(_buttonActions[i]);
        }
    }

    private void OnClicked(int numberButton)
    {
        T type = _unitTacticsTypes[numberButton];
        ButtonClicked?.Invoke(type);
    }

    public virtual void SetButtons(IReadOnlyDictionary<T, Sprite> images, List<T> orderedTypes)
    {
        _unitTacticsTypes = orderedTypes;

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].gameObject.SetActive(false || i < orderedTypes.Count);

            if (i < orderedTypes.Count)
                _buttons[i].GetComponent<Image>().sprite = images[orderedTypes[i]];
        }
    }

    public void TurnOffButtons()
    {
        Debug.Log(":LLLLLLLLLLLLLL");
        Debug.Log(_buttons.Length);
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].gameObject.SetActive(false);
        }
    }

}