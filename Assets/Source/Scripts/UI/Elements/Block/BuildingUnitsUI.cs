
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
    [SerializeField] private GameObject iconPrefab;

    [SerializeField] private Transform iconPanelParent;

    [SerializeField] private UIUnitsConfig uiUnitsConfig;

    [SerializeField] private int maxIcons = 5;

    [Inject] private MissionData _missionData;

    [Inject] private UnitsSelector _unitsSelector;

    private List<GameObject> iconPool = new List<GameObject>();

    private IHiderConstruction currentHiderConstruction;

    private void Start()
    {
        if (_missionData.ConstructionSelector == null)
        {
            Debug.LogError("Селектор построек не найден в данных миссии.");
            return;
        }

        InitializeIconPool();

        _missionData.ConstructionSelector.OnSelectionChange += UpdateBuildingUnitIcons;
    }

    private void InitializeIconPool()
    {
        for (int i = 0; i < maxIcons; i++)
        {
            GameObject iconInstance = Instantiate(iconPrefab, iconPanelParent);

            Button iconButton = iconInstance.AddComponent<Button>();
            int index = i; 
            iconButton.onClick.AddListener(() => ExtractUnit(index));

            iconInstance.SetActive(false);
            iconPool.Add(iconInstance);
        }
    }

    private void OnDestroy()
    {
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateBuildingUnitIcons;
        }

        foreach (GameObject iconInstance in iconPool)
        {
            Button button = iconInstance.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }

    private void UpdateBuildingUnitIcons()
    {
        foreach (GameObject icon in iconPool)
        {
            icon.SetActive(false);
        }

        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        if (selectedConstruction == null || !(selectedConstruction is IHiderConstruction))
        {
            currentHiderConstruction = null;
            _unitsSelector.DeselectAll();
            return;
        }

        currentHiderConstruction = (IHiderConstruction)selectedConstruction;

        IReadOnlyList<HiderCellBase> hiddenUnits = currentHiderConstruction.Hider.HiderCells;

        for (int i = 0; i < Mathf.Min(hiddenUnits.Count, maxIcons); i++)
        {
            UpdateUnitIcon(hiddenUnits[i], iconPool[i]);
        }
    }

    private void UpdateUnitIcon(HiderCellBase unit, GameObject iconInstance)
    {
        if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
        {
            Image[] images = iconInstance.GetComponentsInChildren<Image>();
            if (images.Length >= 2)
            {

                images[1].sprite = unitConfig.InfoSprite;
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

    private void ExtractUnit(int index)
    {
        if (currentHiderConstruction?.Hider == null ||
            currentHiderConstruction.Hider.HiderCells.Count <= index)
        {
            Debug.LogWarning("Нет доступного юнита для извлечения по указанному индексу.");
            return;
        }

        Vector3 extractPosition = GetRandomExtractPosition(currentHiderConstruction);

        UnitBase extractedUnit = currentHiderConstruction.Hider.ExtractUnit(index, extractPosition);

        if (extractedUnit != null)
        {
            Debug.Log($"Юнит {extractedUnit.UnitType} извлечен из здания.");
        }
        else
        {
            Debug.LogWarning("Не удалось извлечь юнита.");
        }

        UpdateBuildingUnitIcons();
    }

    private Vector3 GetRandomExtractPosition(IHiderConstruction hiderConstruction)
    {
        Vector3 buildingPosition = ((MonoBehaviour)hiderConstruction).transform.position;

        float radius = 2f;

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        return buildingPosition + new Vector3(
            Mathf.Cos(angle) * radius,
            0,
            Mathf.Sin(angle) * radius
        );
    }
}