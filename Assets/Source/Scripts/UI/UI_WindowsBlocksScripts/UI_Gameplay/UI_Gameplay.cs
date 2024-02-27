using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gameplay : UIScreen
{
    private GameObject _UI_Buildings;
    private GameObject _UI_Tactics;
    private GameObject _UI_TownHallMenu;
    private GameObject _UI_BarracksMenu;
    private GameObject _UI_BeeHouseMenu;
    private GameObject _UI_BeesWaxProduceConstructionMenu;

    private Dictionary<string, Action> _gameplayWindowsSetActions;

    private ConstructionBase _construction;

    private void Start()
    {
        _gameplayWindowsSetActions = new Dictionary<string, Action>();

        _gameplayWindowsSetActions.Add("UI_GameplayMain", () => { });
        _gameplayWindowsSetActions.Add("UI_Buildings", () => { _UI_Buildings.SetActive(true); });
        _gameplayWindowsSetActions.Add("UI_Tactics", () => { _UI_Tactics.SetActive(true); });
        _gameplayWindowsSetActions.Add("UI_TownHallMenu", () => {
            _UI_TownHallMenu.SetActive(true);
            _UI_TownHallMenu.GetComponent<UI_BeeTownHallMenu>()._CallMenu(_construction);
        });
        _gameplayWindowsSetActions.Add("UI_BarracksMenu", () => {
            _UI_BarracksMenu.SetActive(true);
            _UI_BarracksMenu.GetComponent<UI_BeeBarracksMenu>()._CallMenu(_construction);
        });
        _gameplayWindowsSetActions.Add("UI_BeeHouseMenu", () => {
            _UI_BeeHouseMenu.SetActive(true);
            _UI_BeeHouseMenu.GetComponent<UI_BeeHouseMenu>()._CallMenu(_construction);
        });
        _gameplayWindowsSetActions.Add("UI_BeesWaxProduceConstructionMenu", () => {
            _UI_BeesWaxProduceConstructionMenu.SetActive(true);
            _UI_BeesWaxProduceConstructionMenu.GetComponent<UI_BeesWaxProduceConstructionMenu>()._CallMenu(_construction);
        });
    
        _UI_Buildings = UIScreenRepository.GetScreen<UI_Buildings>().gameObject;
        _UI_Tactics = UIScreenRepository.GetScreen<UI_Tactics>().gameObject;
        _UI_TownHallMenu = UIScreenRepository.GetScreen<UI_BeeTownHallMenu>().gameObject;
        _UI_BarracksMenu = UIScreenRepository.GetScreen<UI_BeeBarracksMenu>().gameObject;
        _UI_BeeHouseMenu = UIScreenRepository.GetScreen<UI_BeeHouseMenu>().gameObject;
        _UI_BeesWaxProduceConstructionMenu = UIScreenRepository.GetScreen<UI_BeesWaxProduceConstructionMenu>().gameObject;
    }

 
    public void SetGameplayWindow(string gameplayWindowName, ConstructionBase construction)
    {
        _construction = construction;
        _UI_Buildings.SetActive(false);
        _UI_Tactics.SetActive(false);
        _UI_TownHallMenu.SetActive(false);
        _UI_BarracksMenu.SetActive(false);
        _UI_BeeHouseMenu.SetActive(false);
        _UI_BeesWaxProduceConstructionMenu.SetActive(false);

        _gameplayWindowsSetActions[gameplayWindowName]?.Invoke();
    }
}
