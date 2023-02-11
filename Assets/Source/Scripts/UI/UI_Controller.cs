using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] Test_Builder_TownHall builder;//необходимо для создания зданий. Т.к. у нас нет обще игрового скрипта, 
                                                   //который мог бы отвечать за спавн и перемещение зданий, то пока что будет эта заглушка.
    GameObject UI_Activ;//текущее активное окно. необходимо для работы _SetWindow()
    UI_Gameplay UI_GameplayWindows;//скрипт который установлен на префабе окна геймплея(UI_Gameplay), нужен просто для удобства и оптимизации, чтобы не вызывать GetComponent<>(): 
                                   //благодаря этому в функции _SetWindow() вместо этого:
                                   //UIScreenRepository.GetScreen<UI_Gameplay>().gameObject.GetComponent<UI_Gameplay>()._SetGameplayWindow(windowName); 
                                   //используется это:
                                   //UI_GameplayWindows._SetGameplayWindow(windowName);
    GameObject UI_PrevActiv;//предыдущее активное окно. необходимо для корректной работы "Back" в _SetWindow()
    GameObject buffer;//буфер. необходимо для корректной работы "Back" в _SetWindow()
    GameObject building;//текущее выделенное здание
    UnitPool pool;
    GameObject currentWorker;

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
    
    public void _SpawnTownHall()//спавн здания. принимает индекс ячейки в List<GameObject> buildings
    {
        builder._SpawnBuilding(ConstructionID.Town_Hall);
    }
    
    public void _SpawnBarrack()//спавн здания. принимает индекс ячейки в List<GameObject> buildings
    {
        builder._SpawnBuilding(ConstructionID.Barrack);
    }
    
    public void _SpawnWaxFactory()//спавн здания. принимает индекс ячейки в List<GameObject> buildings
    {
        builder._SpawnBuilding(ConstructionID.Bees_Wax_Produce_Construction);
    }

    public void _ChoiceTactic()//выбор тактики. Функция пуста т.к. тактик у нас нет и хз как они будут работать
    { Debug.Log("Error: tactics is empty"); }

    public void _ChoiceGroup()
    {
                 
    }

    public void _SetWindow(string windowName)//смена активного окна UI. принимает название окна, которое надо сделать активным
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

    public void _SetBuilding(GameObject newBuilding, ConstructionID constructionID)//установка текущего выделеного здания здания
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
                windowName = "";
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
}