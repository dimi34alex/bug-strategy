using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] List<GameObject> buildings;//массив префабов зданий
    GameObject UI_Activ;//текущее активное окно. необходимо для работы _SetWindow()
    UI_Gameplay UI_GameplayWindows;//скрипт который установлен на префабе окна геймплея(UI_Gameplay), нужен просто для удобства и оптимизации, чтобы не вызывать GetComponent<>(): 
                                   //благодаря этому в функции _SetWindow() вместо этого:
                                   //UIScreenRepository.GetScreen<UI_Gameplay>().gameObject.GetComponent<UI_Gameplay>()._SetGameplayWindow(windowName); 
                                   //используется это:
                                   //UI_GameplayWindows._SetGameplayWindow(windowName);
    GameObject UI_PrevActiv;//предыдущее активное окно. необходимо для корректной работы "Back" в _SetWindow()
    GameObject buffer;//буфер. необходимо для корректной работы "Back" в _SetWindow()
    GameObject building;

    void Start()
    {
        UI_GameplayWindows = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject.GetComponent<UI_Gameplay>();

        //определяем, какое окно у нас активно при запуске.
        if (UIScreenRepository.GetScreen<UI_Gameplay>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_Buildings>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Buildings>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_Tactics>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Tactics>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_GameplayMenu>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_GameplayMenu>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_Settings>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Settings>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_Win>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Win>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_Lose>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Lose>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_MainMenu>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_MainMenu>().gameObject;
        else
        if (UIScreenRepository.GetScreen<UI_Saves>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Saves>().gameObject;
    }

    public void _SpawnBuilding(int number)//спавн здания. принимает индекс ячейки в List<GameObject> buildings
    {
        Instantiate(buildings[number]);
    }

    public void _ChoiceTactic()//выбор тактики. Функция пуста т.к. тактик у нас нет и хз как они будут работать
    { Debug.Log("Error: tactics is empty"); }

    public void _ChoiceGroup()//выбор группы. Функция пуста т.к. групп у нас нет и хз как они будут работать
    { Debug.Log("Error: groups is empty"); }

    public void _SetWindow(string windowName)//смена активного окна UI. принимает название окна, которое надо сделать активным
    {
        buffer = UI_Activ;
        UI_Activ.SetActive(false);

        switch (windowName)
        {
            case "UI_Gameplay":
                UI_Activ = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject; break;

            case "UI_GameplayMain":
                UI_GameplayWindows._SetGameplayWindow(windowName); break;
            case "UI_Buildings":
                UI_GameplayWindows._SetGameplayWindow(windowName); break;
            case "UI_Tactics":
                UI_GameplayWindows._SetGameplayWindow(windowName); break;
            case "UI_TownHallMenu":
                UI_GameplayWindows._SetGameplayWindow(windowName); break;
            case "UI_BarracksMenu":
                UI_GameplayWindows._SetGameplayWindow(windowName); break;

            case "UI_GameplayMenu":
                UI_Activ = UIScreenRepository.GetScreen<UI_GameplayMenu>().gameObject; break;
            case "UI_Settings":
                UI_Activ = UIScreenRepository.GetScreen<UI_Settings>().gameObject; break;
            case "UI_Win":
                UI_Activ = UIScreenRepository.GetScreen<UI_Win>().gameObject; break;
            case "UI_Lose":
                UI_Activ = UIScreenRepository.GetScreen<UI_Lose>().gameObject; break;
            case "UI_MainMenu":
                UI_Activ = UIScreenRepository.GetScreen<UI_MainMenu>().gameObject; break;
            case "UI_Saves":
                UI_Activ = UIScreenRepository.GetScreen<UI_Saves>().gameObject; break;

            case "Back":
                UI_Activ = UI_PrevActiv; break;
            default:
                Debug.Log("Error: invalid string parametr in   _SetWindow(string windowName)"); break;
        }

        UI_PrevActiv = buffer;
        UI_Activ.SetActive(true);
    }

    public void _LoadScene(string sceneName)//загрузка сцены. принимает название сцены
    {
        if (sceneName == "empty")
        {
            Debug.Log("Error: scene name is not set");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void _Quite()//выход из игры
    {
        Application.Quit();
    }


    #region  BuildingsBase

    public void _SetBuilding(GameObject newBuilding)
    {
        building = newBuilding;
    }
    public void _BuildingDestroy()//снос здания. Функция пуста т.к. зданий у нас нет и хз как они будут работать
    {
        building?.GetComponent<BuildingBase>()._DestroyBuilding();
        _SetWindow("UI_GameplayMain");
    }
    public void _BuildingLVL_Up()//повышение уровня здания. Функция пуста т.к. зданий у нас нет и хз как они будут работать
    {
        building?.GetComponent<BuildingBase>()._LVL_UpBuilding();
    }
    public void _BuildingReplace()//перемещение здания. Функция пуста т.к. зданий у нас нет и хз как они будут работать
    {
        building?.GetComponent<BuildingBase>()._ReplaceBuilding();
    }

    #endregion


    #region  TownHall

    public void _SpawnWorkerBee()
    {
        building.GetComponent<TownHall>()._SpawnWorkerBee();
    }
    public void _WorkerBeeAlarmer()
    {
        building.GetComponent<TownHall>()._WorkerBeeAlarmer();
    }
    
    #endregion


}