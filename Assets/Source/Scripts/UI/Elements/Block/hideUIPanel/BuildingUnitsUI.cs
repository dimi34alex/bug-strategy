using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.UnitsHideCore;
using BugStrategy.Constructions;
using Zenject;
using BugStrategy.Missions;

public class BuildingUnitsUI : MonoBehaviour
{
    // Префаб для иконки юнита
    [SerializeField] private GameObject iconPrefab;
    // Родительский объект для размещения иконки
    [SerializeField] private Transform iconPanelParent;
    // Конфигурация UI для юнитов
    [SerializeField] private UIUnitsConfig uiUnitsConfig;

    // Инжектируемые данные миссии
    [Inject] private MissionData _missionData;

    // Текущая активная иконка
    private GameObject activeIcon;
    // Текущее здание, способное скрывать юнитов
    private IHiderConstruction currentHiderConstruction;

    private void Start()
    {
        // Проверяем наличие селектора построек
        if (_missionData.ConstructionSelector == null)
        {
            Debug.LogError("ConstructionSelector не найден в MissionData.");
        }
        else
        {
            // Подписываемся на событие изменения выбора постройки
            _missionData.ConstructionSelector.OnSelectionChange += UpdateBuildingUnitIcon;
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateBuildingUnitIcon;
        }
    }

    private void Update()
    {
        // Проверяем нажатие клавиши 'O'
        if (Input.GetKeyDown(KeyCode.O))
        {
            ExtractUnit();
        }
    }

    private void UpdateBuildingUnitIcon()
    {
        // Удаляем предыдущую иконку, если она есть
        if (activeIcon != null)
        {
            Destroy(activeIcon);
            activeIcon = null;
        }

        // Получаем выбранную постройку
        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        // Проверяем, является ли постройка способной скрывать юнитов
        if (selectedConstruction == null || !(selectedConstruction is IHiderConstruction))
        {
            currentHiderConstruction = null;
            return;
        }

        // Сохраняем текущую постройку
        currentHiderConstruction = (IHiderConstruction)selectedConstruction;

        // Получаем информацию о скрытом юните
        IReadOnlyList<HiderCellBase> hiddenUnits = currentHiderConstruction.Hider.HiderCells;

        // Если есть скрытый юнит, отображаем его иконку
        if (hiddenUnits.Count > 0)
        {
            DisplayUnitIcon(hiddenUnits[0]);
        }
    }

    private void DisplayUnitIcon(HiderCellBase unit)
    {
        // Проверяем наличие конфигурации UI для данного типа юнита
        if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
        {
            // Создаем новую иконку
            activeIcon = Instantiate(iconPrefab, iconPanelParent);

            // Настраиваем спрайт иконки
            Image[] images = activeIcon.GetComponentsInChildren<Image>();
            if (images.Length >= 2)
            {
                images[1].sprite = unitConfig.InfoSprite;
            }
            else
            {
                Debug.LogError("Недостаточно компонентов Image в префабе иконки. Ожидалось как минимум 2.");
            }
        }
        else
        {
            Debug.LogWarning($"Не найдена конфигурация UI для типа юнита: {unit.UnitType}");
        }
    }

    private void ExtractUnit()
    {
        // Проверяем, есть ли выбранное здание с юнитом
        if (currentHiderConstruction == null || currentHiderConstruction.Hider == null)
        {
            Debug.LogWarning("Нет выбранного здания с юнитом для извлечения.");
            return;
        }

        IHider hider = currentHiderConstruction.Hider;

        // Проверяем, есть ли юнит в здании
        if (hider.HiderCells.Count == 0)
        {
            Debug.LogWarning("В здании нет юнитов для извлечения.");
            return;
        }

        // Получаем позицию для извлечения юнита
        Vector3 extractPosition = GetRandomExtractPosition(currentHiderConstruction);

        // Извлекаем юнит
        UnitBase extractedUnit = hider.ExtractUnit(0, extractPosition);

        if (extractedUnit != null)
        {
            Debug.Log($"Юнит {extractedUnit.UnitType} извлечен из здания.");
        }
        else
        {
            Debug.LogWarning("Не удалось извлечь юнита.");
        }

        // Обновляем UI после извлечения юнита
        UpdateBuildingUnitIcon();
    }

    private Vector3 GetRandomExtractPosition(IHiderConstruction hiderConstruction)
    {
        // Получаем позицию здания
        Vector3 buildingPosition = ((MonoBehaviour)hiderConstruction).transform.position;

        // Генерируем случайное смещение в пределах круга
        float radius = 2f; // Радиус круга вокруг здания
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // Случайный угол в радианах
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Возвращаем позицию здания плюс случайное смещение
        return buildingPosition + new Vector3(x, 0, z);
    }
}
