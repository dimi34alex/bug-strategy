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
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconPanelParent;
    [SerializeField] private UIUnitsConfig uiUnitsConfig;

    [Inject] private MissionData _missionData;

    private GameObject iconInstance;
    private Image iconImage;
    private Button iconButton;
    private IHiderConstruction currentHiderConstruction;

    private void Start()
    {
        if (_missionData.ConstructionSelector == null)
        {
            Debug.LogError("ConstructionSelector �� ������ � MissionData.");
            return;
        }

        InitializeIcon();
        _missionData.ConstructionSelector.OnSelectionChange += UpdateBuildingUnitIcon;
    }

    private void InitializeIcon()
    {
        iconInstance = Instantiate(iconPrefab, iconPanelParent);

        // �������� ����������
        Image[] images = iconInstance.GetComponentsInChildren<Image>();
        if (images.Length >= 2)
        {
            iconImage = images[1];
        }
        else
        {
            Debug.LogError("������������ ����������� Image � ������� ������. ��������� ��� ������� 2.");
        }

        // ��������� � ����������� ������
        iconButton = iconInstance.AddComponent<Button>();
        iconButton.onClick.AddListener(ExtractUnit);

        // ���������� �������� ������
        iconInstance.SetActive(false);
    }

    private void OnDestroy()
    {
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateBuildingUnitIcon;
        }

        if (iconButton != null)
        {
            iconButton.onClick.RemoveListener(ExtractUnit);
        }
    }

    private void UpdateBuildingUnitIcon()
    {
        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        if (selectedConstruction == null || !(selectedConstruction is IHiderConstruction))
        {
            currentHiderConstruction = null;
            iconInstance.SetActive(false);
            return;
        }

        currentHiderConstruction = (IHiderConstruction)selectedConstruction;
        IReadOnlyList<HiderCellBase> hiddenUnits = currentHiderConstruction.Hider.HiderCells;

        if (hiddenUnits.Count > 0)
        {
            UpdateUnitIcon(hiddenUnits[0]);
        }
        else
        {
            iconInstance.SetActive(false);
        }
    }

    private void UpdateUnitIcon(HiderCellBase unit)
    {
        if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
        {
            iconImage.sprite = unitConfig.InfoSprite;
            iconInstance.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"�� ������� ������������ UI ��� ���� �����: {unit.UnitType}");
            iconInstance.SetActive(false);
        }
    }

    private void ExtractUnit()
    {
        if (currentHiderConstruction?.Hider == null || currentHiderConstruction.Hider.HiderCells.Count == 0)
        {
            Debug.LogWarning("��� ���������� ����� ��� ����������.");
            return;
        }

        Vector3 extractPosition = GetRandomExtractPosition(currentHiderConstruction);
        UnitBase extractedUnit = currentHiderConstruction.Hider.ExtractUnit(0, extractPosition);

        if (extractedUnit != null)
        {
            Debug.Log($"���� {extractedUnit.UnitType} �������� �� ������.");
        }
        else
        {
            Debug.LogWarning("�� ������� ������� �����.");
        }

        UpdateBuildingUnitIcon();
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