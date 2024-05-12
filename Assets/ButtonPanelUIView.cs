using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonPanelUIView<T> : UIScreen
{
    private Button[] _tacticButtons;
    private List<T> _unitTacticsTypes;

    private List<UnityAction> _buttonActions = new List<UnityAction>();

    public event Action<T> ButtonClicked;

    private void Awake()
    {
        _tacticButtons = gameObject.GetComponentsInChildren<Button>(true);

        for (int i =0; i < _tacticButtons.Length; i++)
        {
            int iValue = i; // убираю "ссылочность" i, не трогать )

            UnityAction action = () => { OnClicked(iValue); };

            _buttonActions.Add(action);
        }

        for (int i = 0; i < _tacticButtons.Length; i++)
        {
            _tacticButtons[i].onClick.AddListener(_buttonActions[i]);
        }
    }

    private void OnClicked(int numberButton)
    {
        T type = _unitTacticsTypes[numberButton];
        ButtonClicked?.Invoke(type);
    }

    public void SetButtons(IReadOnlyDictionary<T, Sprite> images, List<T> orderedTypes)
    {
        _unitTacticsTypes = orderedTypes;

        for (int i = 0; i < _tacticButtons.Length; i++)
        {
            _tacticButtons[i].gameObject.SetActive(false || i < orderedTypes.Count);

            if (i < orderedTypes.Count)
                _tacticButtons[i].GetComponent<Image>().sprite = images[orderedTypes[i]];
        }
    }

    public void TurnOffButtons()
    {
        for (int i = 0; i < _tacticButtons.Length; i++)
        {
            _tacticButtons[i].gameObject.SetActive(false);
        }
    }

}