
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.UnitsHideCore;
using BugStrategy.Constructions;
using Zenject;
using BugStrategy.Missions;
using BugStrategy.Unit.UnitSelection;

public class BuildingUnitsUI : MonoBehaviour
{
    // Префаб иконки юнита
    [SerializeField] private GameObject iconPrefab;

    // Родительский Transform для размещения иконок
    [SerializeField] private Transform iconPanelParent;

    // Конфигурация UI для юнитов
    [SerializeField] private UIUnitsConfig uiUnitsConfig;

    // Максимальное количество отображаемых иконок
    [SerializeField] private int maxIcons = 5;

    // Инъекция зависимостей для получения данных миссии
    [Inject] private MissionData _missionData;

    // Инъекция селектора юнитов
    [Inject] private UnitsSelector _unitsSelector;

    // Пул иконок для переиспользования
    private List<GameObject> iconPool = new List<GameObject>();

    // Текущее здание-укрыватель
    private IHiderConstruction currentHiderConstruction;

    private void Start()
    {
        // Проверка наличия селектора построек
        if (_missionData.ConstructionSelector == null)
        {
            Debug.LogError("Селектор построек не найден в данных миссии.");
            return;
        }

        // Инициализация пула иконок
        InitializeIconPool();

        // Подписка на событие изменения выделения построек
        _missionData.ConstructionSelector.OnSelectionChange += UpdateBuildingUnitIcons;
    }

    // Создание пула иконок для юнитов
    private void InitializeIconPool()
    {
        // Создаем указанное количество иконок
        for (int i = 0; i < maxIcons; i++)
        {
            // Создаем экземпляр иконки
            GameObject iconInstance = Instantiate(iconPrefab, iconPanelParent);

            // Добавляем кнопку к каждой иконке
            Button iconButton = iconInstance.AddComponent<Button>();
            int index = i; // Захватываем текущий индекс для лямбда-выражения
            iconButton.onClick.AddListener(() => ExtractUnit(index));

            // Первоначально скрываем иконку
            iconInstance.SetActive(false);
            iconPool.Add(iconInstance);
        }
    }

    // Очистка при уничтожении объекта
    private void OnDestroy()
    {
        // Отписываемся от события изменения селектора
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateBuildingUnitIcons;
        }

        // Удаляем все обработчики кликов
        foreach (GameObject iconInstance in iconPool)
        {
            Button button = iconInstance.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }

    // Обновление иконок юнитов в здании
    private void UpdateBuildingUnitIcons()
    {
        // Сначала деактивируем все иконки
        foreach (GameObject icon in iconPool)
        {
            icon.SetActive(false);
        }

        // Получаем выбранную постройку
        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        // Проверяем, является ли постройка хранилищем юнитов
        if (selectedConstruction == null || !(selectedConstruction is IHiderConstruction))
        {
            currentHiderConstruction = null;
            _unitsSelector.DeselectAll();
            return;
        }

        // Устанавливаем текущее здание-укрыватель
        currentHiderConstruction = (IHiderConstruction)selectedConstruction;

        // Получаем список скрытых юнитов
        IReadOnlyList<HiderCellBase> hiddenUnits = currentHiderConstruction.Hider.HiderCells;

        // Отображаем до maxIcons количества юнитов
        for (int i = 0; i < Mathf.Min(hiddenUnits.Count, maxIcons); i++)
        {
            UpdateUnitIcon(hiddenUnits[i], iconPool[i]);
        }
    }

    // Обновление иконки конкретного юнита
    private void UpdateUnitIcon(HiderCellBase unit, GameObject iconInstance)
    {
        // Ищем конфигурацию UI для типа юнита
        if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
        {
            // Получаем компоненты Image
            Image[] images = iconInstance.GetComponentsInChildren<Image>();
            if (images.Length >= 2)
            {
                // Устанавливаем спрайт юнита
                images[1].sprite = unitConfig.InfoSprite;
                // Делаем иконку видимой
                iconInstance.SetActive(true);
            }
            else
            {
                Debug.LogError("Недостаточно компонентов Image в префабе иконки.");
            }
        }
        else
        {
            Debug.LogWarning($"Не найдена конфигурация UI для типа юнита: {unit.UnitType}");
        }
    }

    // Извлечение юнита по индексу
    private void ExtractUnit(int index)
    {
        // Проверяем наличие юнита для извлечения
        if (currentHiderConstruction?.Hider == null ||
            currentHiderConstruction.Hider.HiderCells.Count <= index)
        {
            Debug.LogWarning("Нет доступного юнита для извлечения по указанному индексу.");
            return;
        }

        // Получаем случайную позицию для извлечения
        Vector3 extractPosition = GetRandomExtractPosition(currentHiderConstruction);

        // Извлекаем юнит
        UnitBase extractedUnit = currentHiderConstruction.Hider.ExtractUnit(index, extractPosition);

        // Логирование результата
        if (extractedUnit != null)
        {
            Debug.Log($"Юнит {extractedUnit.UnitType} извлечен из здания.");
        }
        else
        {
            Debug.LogWarning("Не удалось извлечь юнита.");
        }

        // Обновляем отображение иконок
        UpdateBuildingUnitIcons();
    }

    // Получение случайной позиции для извлечения юнита
    private Vector3 GetRandomExtractPosition(IHiderConstruction hiderConstruction)
    {
        // Получаем позицию здания
        Vector3 buildingPosition = ((MonoBehaviour)hiderConstruction).transform.position;

        // Радиус области извлечения
        float radius = 2f;

        // Случайный угол
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        // Возвращаем случайную точку вокруг здания
        return buildingPosition + new Vector3(
            Mathf.Cos(angle) * radius,
            0,
            Mathf.Sin(angle) * radius
        );
    }
}