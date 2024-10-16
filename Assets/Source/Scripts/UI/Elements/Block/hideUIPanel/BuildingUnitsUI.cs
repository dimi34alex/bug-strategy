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
    // ������ ��� ������ �����
    [SerializeField] private GameObject iconPrefab;
    // ������������ ������ ��� ���������� ������
    [SerializeField] private Transform iconPanelParent;
    // ������������ UI ��� ������
    [SerializeField] private UIUnitsConfig uiUnitsConfig;

    // ������������� ������ ������
    [Inject] private MissionData _missionData;

    // ������� �������� ������
    private GameObject activeIcon;
    // ������� ������, ��������� �������� ������
    private IHiderConstruction currentHiderConstruction;

    private void Start()
    {
        // ��������� ������� ��������� ��������
        if (_missionData.ConstructionSelector == null)
        {
            Debug.LogError("ConstructionSelector �� ������ � MissionData.");
        }
        else
        {
            // ������������� �� ������� ��������� ������ ���������
            _missionData.ConstructionSelector.OnSelectionChange += UpdateBuildingUnitIcon;
        }
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateBuildingUnitIcon;
        }
    }

    private void Update()
    {
        // ��������� ������� ������� 'O'
        if (Input.GetKeyDown(KeyCode.O))
        {
            ExtractUnit();
        }
    }

    private void UpdateBuildingUnitIcon()
    {
        // ������� ���������� ������, ���� ��� ����
        if (activeIcon != null)
        {
            Destroy(activeIcon);
            activeIcon = null;
        }

        // �������� ��������� ���������
        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        // ���������, �������� �� ��������� ��������� �������� ������
        if (selectedConstruction == null || !(selectedConstruction is IHiderConstruction))
        {
            currentHiderConstruction = null;
            return;
        }

        // ��������� ������� ���������
        currentHiderConstruction = (IHiderConstruction)selectedConstruction;

        // �������� ���������� � ������� �����
        IReadOnlyList<HiderCellBase> hiddenUnits = currentHiderConstruction.Hider.HiderCells;

        // ���� ���� ������� ����, ���������� ��� ������
        if (hiddenUnits.Count > 0)
        {
            DisplayUnitIcon(hiddenUnits[0]);
        }
    }

    private void DisplayUnitIcon(HiderCellBase unit)
    {
        // ��������� ������� ������������ UI ��� ������� ���� �����
        if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
        {
            // ������� ����� ������
            activeIcon = Instantiate(iconPrefab, iconPanelParent);

            // ����������� ������ ������
            Image[] images = activeIcon.GetComponentsInChildren<Image>();
            if (images.Length >= 2)
            {
                images[1].sprite = unitConfig.InfoSprite;
            }
            else
            {
                Debug.LogError("������������ ����������� Image � ������� ������. ��������� ��� ������� 2.");
            }
        }
        else
        {
            Debug.LogWarning($"�� ������� ������������ UI ��� ���� �����: {unit.UnitType}");
        }
    }

    private void ExtractUnit()
    {
        // ���������, ���� �� ��������� ������ � ������
        if (currentHiderConstruction == null || currentHiderConstruction.Hider == null)
        {
            Debug.LogWarning("��� ���������� ������ � ������ ��� ����������.");
            return;
        }

        IHider hider = currentHiderConstruction.Hider;

        // ���������, ���� �� ���� � ������
        if (hider.HiderCells.Count == 0)
        {
            Debug.LogWarning("� ������ ��� ������ ��� ����������.");
            return;
        }

        // �������� ������� ��� ���������� �����
        Vector3 extractPosition = GetRandomExtractPosition(currentHiderConstruction);

        // ��������� ����
        UnitBase extractedUnit = hider.ExtractUnit(0, extractPosition);

        if (extractedUnit != null)
        {
            Debug.Log($"���� {extractedUnit.UnitType} �������� �� ������.");
        }
        else
        {
            Debug.LogWarning("�� ������� ������� �����.");
        }

        // ��������� UI ����� ���������� �����
        UpdateBuildingUnitIcon();
    }

    private Vector3 GetRandomExtractPosition(IHiderConstruction hiderConstruction)
    {
        // �������� ������� ������
        Vector3 buildingPosition = ((MonoBehaviour)hiderConstruction).transform.position;

        // ���������� ��������� �������� � �������� �����
        float radius = 2f; // ������ ����� ������ ������
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad; // ��������� ���� � ��������
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // ���������� ������� ������ ���� ��������� ��������
        return buildingPosition + new Vector3(x, 0, z);
    }
}
