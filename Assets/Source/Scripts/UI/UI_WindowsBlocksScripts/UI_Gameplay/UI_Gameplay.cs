using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gameplay : UIScreen
{
    GameObject _UI_Buildings;
    GameObject _UI_Tactics;
    GameObject _UI_TownHallMenu;
    GameObject _UI_BarracksMenu;
    GameObject _UI_BeeHouseMenu;
    GameObject _UI_BeesWaxProduceConstructionMenu;

    void Start()
    {
        _UI_Buildings = UIScreenRepository.GetScreen<UI_Buildings>().gameObject;
        _UI_Tactics = UIScreenRepository.GetScreen<UI_Tactics>().gameObject;
        _UI_TownHallMenu = UIScreenRepository.GetScreen<UI_TownHallMenu>().gameObject;
        _UI_BarracksMenu = UIScreenRepository.GetScreen<UI_BarracksMenu>().gameObject;
        _UI_BeeHouseMenu = UIScreenRepository.GetScreen<UI_BeeHouseMenu>().gameObject;
        _UI_BeesWaxProduceConstructionMenu = UIScreenRepository.GetScreen<UI_BeesWaxProduceConstructionMenu>().gameObject;
    }

    GameObject UI_Activ;//текущее активное окно. необходимо для работы _SetWindow()
    public void _SetGameplayWindow(string gameplayWindowName, ConstructionBase construction)
    {
        switch (gameplayWindowName)
        {
            case "UI_GameplayMain":
                {
                    _UI_Buildings.SetActive(false);
                    _UI_Tactics.SetActive(false);
                    _UI_TownHallMenu.SetActive(false);
                    _UI_BarracksMenu.SetActive(false);
                    _UI_BeeHouseMenu.SetActive(false);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(false);
                    break;
                }
            case "UI_Buildings":
                {
                    _UI_Buildings.SetActive(true);
                    _UI_Tactics.SetActive(false);
                    _UI_TownHallMenu.SetActive(false);
                    _UI_BarracksMenu.SetActive(false);
                    _UI_BeeHouseMenu.SetActive(false);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(false);
                    break;
                }
            case "UI_Tactics":
                {
                    _UI_Buildings.SetActive(false);
                    _UI_Tactics.SetActive(true);
                    _UI_TownHallMenu.SetActive(false);
                    _UI_BarracksMenu.SetActive(false);
                    _UI_BeeHouseMenu.SetActive(false);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(false);
                    break;
                }
            case "UI_TownHallMenu":
                {
                    _UI_Buildings.SetActive(false);
                    _UI_Tactics.SetActive(false);
                    _UI_TownHallMenu.SetActive(true);
                    _UI_TownHallMenu.GetComponent<UI_TownHallMenu>()._CallMenu(construction);
                    _UI_BarracksMenu.SetActive(false);
                    _UI_BeeHouseMenu.SetActive(false);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(false);
                    break;
                }
            case "UI_BarracksMenu":
                {
                    _UI_Buildings.SetActive(false);
                    _UI_Tactics.SetActive(false);
                    _UI_TownHallMenu.SetActive(false);
                    _UI_BarracksMenu.SetActive(true);
                    _UI_BarracksMenu.GetComponent<UI_BarracksMenu>()._CallMenu(construction);
                    _UI_BeeHouseMenu.SetActive(false);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(false);
                    break;
                }
            case "UI_BeeHouseMenu":
                {
                    _UI_Buildings.SetActive(false);
                    _UI_Tactics.SetActive(false);
                    _UI_TownHallMenu.SetActive(false);
                    _UI_BarracksMenu.SetActive(false);
                    _UI_BeeHouseMenu.SetActive(true);
                    _UI_BeeHouseMenu.GetComponent<UI_BeeHouseMenu>()._CallMenu(construction);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(false);
                    break;
                }
            case "UI_BeesWaxProduceConstructionMenu":
                {
                    _UI_Buildings.SetActive(false);
                    _UI_Tactics.SetActive(false);
                    _UI_TownHallMenu.SetActive(false);
                    _UI_BarracksMenu.SetActive(false);
                    _UI_BeeHouseMenu.SetActive(false);
                    _UI_BeesWaxProduceConstructionMenu.SetActive(true);
                    _UI_BeesWaxProduceConstructionMenu.GetComponent<UI_BeesWaxProduceConstructionMenu>()._CallMenu(construction);
                    break;
                }
            default:
                Debug.Log("Error: invalid string parameter in _SetGameplayWindow(string gemeplayWindowName)"); break;
        }
    }
}
