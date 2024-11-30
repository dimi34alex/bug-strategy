using UnityEngine;
using BugStrategy.Unit.UnitSelection;
using BugStrategy.Missions;
using BugStrategy.Constructions;
using BugStrategy.UnitsHideCore;
using Zenject;

public class UnitPanelSelector : MonoBehaviour
{
    [Inject] private UnitsSelector _unitsSelector;
    [Inject] private MissionData _missionData;

    [SerializeField] private UnitSelectionUI _unitSelectionUI;
    [SerializeField] private BuildingUnitsUI _buildingUnitsUI;

    private void Awake()
    {
        _unitsSelector.OnSelectionChanged += UpdateUnitPanel;
        _missionData.ConstructionSelector.OnSelectionChange += UpdateUnitPanel;
    }

    private void OnDestroy()
    {
        _unitsSelector.OnSelectionChanged -= UpdateUnitPanel;
        _missionData.ConstructionSelector.OnSelectionChange -= UpdateUnitPanel;
    }

    private void UpdateUnitPanel()
    {
        var selectedUnits = _unitsSelector.GetSelectedUnits();
        var selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        if (selectedUnits.Count > 0)
        {
            if (selectedConstruction is IHiderConstruction hiderConstruction)
            {
                ShowBuildingUnitsPanel();
            }
            else
            {
                ShowSelectedUnitsPanel();
            }
        }
        else if (selectedConstruction is IHiderConstruction)
        {
            ShowBuildingUnitsPanel();
        }
        else
        {
            HideBothPanels();
        }
    }

    private void ShowSelectedUnitsPanel()
    {
        _unitSelectionUI.ShowIcons();
        _buildingUnitsUI.HideIcons();
    }

    private void ShowBuildingUnitsPanel()
    {
        _unitSelectionUI.HideIcons();
        _buildingUnitsUI.ShowIcons();
    }

    private void HideBothPanels()
    {
        _unitSelectionUI.HideIcons();
        _buildingUnitsUI.HideIcons();
    }
}