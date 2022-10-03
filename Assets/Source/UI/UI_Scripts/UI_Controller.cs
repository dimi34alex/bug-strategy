using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] List<GameObject> buildings;//массив префабов зданий
    GameObject UI_Activ;//текущее активное окно. необходимо для работы _SetWindow()
    GameObject UI_PrevActiv;//предыдущее активное окно. необходимо для корректной работы "Back" в _SetWindow()
    GameObject Buffer;//буфер. необходимо для корректной работы "Back" в _SetWindow()

    void Awake()
    {
        //определяем, какое окно у нас активно при запуске.
        if (UIScreenRepository.GetScreen<UI_Gameplay>().isActiveAndEnabled)
            UI_Activ = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject;
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

    public void _SetWindow(string windowName)//смена активного окна UI. принимает название окна, которое надо сделать активным
    {
        Buffer = UI_Activ;
        UI_Activ.SetActive(false);

        switch (windowName)
        {
            case "UI_Gameplay":
                UI_Activ = UIScreenRepository.GetScreen<UI_Gameplay>().gameObject; break;
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
                Debug.Log("Error: invalid string parametr in   _SetWindow(string str)"); break;
        }

        UI_PrevActiv = Buffer;
        UI_Activ.SetActive(true);
    }

    public void _LoadScene(string sceneName)//загрузка сцены. принимает название сцены
    {
        SceneManager.LoadScene(sceneName);
    }

    public void _Quite()//выход из игры
    {
        Application.Quit();
    }
}