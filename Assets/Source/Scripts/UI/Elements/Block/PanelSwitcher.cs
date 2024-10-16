using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Constructions;
using Zenject;
using BugStrategy.Missions;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject mainUIPanel; // Основная UI-панель
    [SerializeField] private GameObject hideUIPanel; // Панель, которая показывает спрятанных юнитов
    [SerializeField] private GameObject pressBPanel; // Панель, указывающая на необходимость нажать клавишу "B"

    [Inject] private MissionData _missionData;

    private bool isPressBPressed = false; // Флаг

    private void Start()
    {
        // Проверяем, все ли панели назначены в инспекторе, и выводим ошибку, если какая-то из них не назначена
        if (mainUIPanel == null || hideUIPanel == null || pressBPanel == null)
        {
            Debug.LogError("One or more UI panels are not assigned in the inspector.");
            return;
        }

        // Изначально показываем основную панель и скрываем остальные
        mainUIPanel.SetActive(true);
        hideUIPanel.SetActive(false);
        pressBPanel.SetActive(false);

        // Подписываемся на событие изменения выбора здания, если оно существует
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange += HandleConstructionSelection;
        }
        else
        {
            Debug.LogError("ConstructionSelector not found in MissionData."); // Выводим сообщение об ошибке
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= HandleConstructionSelection;
        }
    }

    private void Update()
    {
        // Проверяем, нажата ли клавиша "B" и выбрано ли здание для переключения панелей
        if (Input.GetKeyDown(KeyCode.B) && _missionData.ConstructionSelector.SelectedConstruction != null)
        {
            TogglePanels(); // Вызываем метод для переключения панелей
            isPressBPressed = true; // Обновляем флаг, указывая, что "B" была нажата
        }
    }

    private void HandleConstructionSelection()
    {
        // Проверяем, выбрано ли здание
        bool isConstructionSelected = _missionData.ConstructionSelector.SelectedConstruction != null;

        // Показываем панель "Press B", если здание выбрано, иначе скрываем её
        if (isConstructionSelected)
        {
            pressBPanel.SetActive(true);
        }
        else
        {
            // Если выделение снято, скрываем панель "Press B" и сбрасываем флаг isPressBPressed
            pressBPanel.SetActive(false);
            isPressBPressed = false;
        }
    }

    private void TogglePanels()
    {
        // Переключаем активность основной и скрываемой панелей
        mainUIPanel.SetActive(!mainUIPanel.activeSelf);
        hideUIPanel.SetActive(!hideUIPanel.activeSelf);

        // Если здание больше не выбрано, скрываем панель "Press B"
        if (_missionData.ConstructionSelector.SelectedConstruction == null)
        {
            pressBPanel.SetActive(false);
        }
    }
}