using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    static Test_Builder_TownHall builder;//необходимо для создания зданий. Т.к. у нас нет обще игрового скрипта, 
                                                   //который мог бы отвечать за спавн и перемещение зданий, то пока что будет эта заглушка.
    static GameObject UI_Activ;//текущее активное окно. необходимо для работы _SetWindow()
    static UI_Gameplay UI_GameplayWindows;//скрипт который установлен на префабе окна геймплея(UI_Gameplay), нужен просто для удобства и оптимизации, чтобы не вызывать GetComponent<>(): 
                                   //благодаря этому в функции _SetWindow() вместо этого:
                                   //UIScreenRepository.GetScreen<UI_Gameplay>().gameObject.GetComponent<UI_Gameplay>()._SetGameplayWindow(windowName); 
                                   //используется это:
                                   //UI_GameplayWindows._SetGameplayWindow(windowName);
    static GameObject UI_PrevActiv;//предыдущее активное окно. необходимо для корректной работы "Back" в _SetWindow()
    static GameObject buffer;//буфер. необходимо для корректной работы "Back" в _SetWindow()
    static GameObject building;//текущее выделенное здание
    static UnitPool pool;
    static GameObject currentWorker;

    void Start()
    {
        builder = GameObject.Find("Builder").GetComponent<Test_Builder_TownHall>();
        if(builder == null)
            Debug.LogError("Builder is null");
        
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

    #region Spawn of buildings
    public static void _SpawnTownHall()
    {
        builder._SpawnBuilding(ConstructionID.Town_Hall);
    }
    
    public static void _SpawnBarrack()
    {
        builder._SpawnBuilding(ConstructionID.Barrack);
    }
    
    public static void _SpawnWaxFactory()
    {
        builder._SpawnBuilding(ConstructionID.Bees_Wax_Produce_Construction);
    }
    #endregion

    public static void _ChoiceTactic()
    { Debug.Log("Error: tactics is empty"); }

    public static void _ChoiceGroup()
    { Debug.Log("Error: groups is empty"); }

    public static void _SetWindow(string windowName)//смена активного окна UI. принимает название окна, которое надо сделать активным
    {
        buffer = UI_Activ;
        UI_Activ.SetActive(false);

        switch (windowName)
        {
            case "UI_Gameplay":
                UI_Activ = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject; break;

            case "UI_GameplayMain":
                UI_GameplayWindows._SetGameplayWindow(windowName, null); break;
            case "UI_Buildings":
                UI_GameplayWindows._SetGameplayWindow(windowName, null); break;
            case "UI_Tactics":
                UI_GameplayWindows._SetGameplayWindow(windowName, null); break;
            case "UI_TownHallMenu":
                UI_GameplayWindows._SetGameplayWindow(windowName, building); break;
            case "UI_BarracksMenu":
                UI_GameplayWindows._SetGameplayWindow(windowName, building); break;
            case "UI_BeesWaxProduceConstructionMenu":
                UI_GameplayWindows._SetGameplayWindow(windowName, building); break;

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
                Debug.Log("Error: invalid string parametr in _SetWindow(string windowName)"); break;
        }

        UI_PrevActiv = buffer;
        UI_Activ.SetActive(true);
    }

    public static void _LoadScene(string sceneName)//загрузка сцены. принимает название сцены
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

    public static void _SetBuilding(GameObject newBuilding, ConstructionID constructionID)//установка текущего выделеного здания здания
    {
        string windowName;
        switch (constructionID)
        {
            case (ConstructionID.Town_Hall):
            {
                windowName = "UI_TownHallMenu";
                break;
            }
            case (ConstructionID.Barrack):
            {
                windowName = "UI_BarracksMenu";
                break;
            }
            case (ConstructionID.Bees_Wax_Produce_Construction):
            {
                windowName = "UI_BeesWaxProduceConstructionMenu";
                break;
            }
            default:
            {
                windowName = "UI_GameplayMain";
                break;
            }
        }
        
        building = newBuilding;
        _SetWindow(windowName);
    }
        UI_Error._ErrorCall(error);
    }
    
    public static void _Quite()
    {
        Application.Quit();
    }
}