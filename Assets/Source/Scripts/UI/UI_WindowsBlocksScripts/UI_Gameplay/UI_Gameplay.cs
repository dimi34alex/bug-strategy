using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gameplay : UIScreen
{
    private GameObject _UI_Buildings;
    private GameObject _UI_Tactics;
    private ConstructionUIController _buldingUIController;

    private ConstructionBase _construction;

    private Dictionary<UIWindowType, Action> _gameplayWindowsSetActions;

    private void Start()
    {
        _gameplayWindowsSetActions = new Dictionary<UIWindowType, Action>();

        _gameplayWindowsSetActions.Add(UIWindowType.GameMain, () => { });
        _gameplayWindowsSetActions.Add(UIWindowType.Building, () => { _UI_Buildings.SetActive(true); });
        _gameplayWindowsSetActions.Add(UIWindowType.Tactics, () => { _UI_Tactics.SetActive(true); });
       // _gameplayWindowsSetActions.Add(UIWindowType.ConstructionMenu, () => { _buldingUIController.SetWindow(_construction); });

        _buldingUIController = UIScreenRepository.GetScreen<ConstructionUIController>();
        _UI_Buildings = UIScreenRepository.GetScreen<UI_Buildings>().gameObject;
        _UI_Tactics = UIScreenRepository.GetScreen<UI_Tactics>().gameObject;
    }
        
 
    public void SetGameplayWindow(UIWindowType type, ConstructionBase construction)
    {
        _construction = construction;

        _UI_Buildings.SetActive(false);
        _UI_Tactics.SetActive(false);

        _buldingUIController.ClearWindow();
        _gameplayWindowsSetActions[type]?.Invoke();
    }
}
