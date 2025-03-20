using System.Collections.Generic;
using UnityEngine;
using BugStrategy.Unit.UnitSelection;
using BugStrategy.Missions;
using BugStrategy.Unit;
using BugStrategy.UnitsHideCore;
using Zenject;

public class UnitPanelSelector : MonoBehaviour
{
    [SerializeField] private UnitSelectionUI unitSelectionUI;
    [SerializeField] private BuildingUnitsUI buildingUnitsUI;
    [SerializeField] private GameObject panel;
    
    [Inject] private PlayerUnitsSelector _playerUnitsSelector;
    [Inject] private MissionData _missionData;

    private void Awake()
    {
        _playerUnitsSelector.OnSelectionChanged += UpdateUnitPanel;
        _missionData.ConstructionSelector.OnSelectionChange += UpdateUnitPanel;
        
        UpdateUnitPanel();
    }

    private void OnDestroy()
    {
        _playerUnitsSelector.OnSelectionChanged -= UpdateUnitPanel;
        _missionData.ConstructionSelector.OnSelectionChange -= UpdateUnitPanel;
    }

    private void UpdateUnitPanel()
    {
        var selectedUnits = _playerUnitsSelector.GetSelectedUnits();
        var selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

        if (selectedUnits.Count > 0)
        {
            if (selectedConstruction is IHiderConstruction hiderConstruction)
            {
                ShowBuildingUnitsPanel(hiderConstruction);
            }
            else
            {
                ShowSelectedUnitsPanel(selectedUnits);
            }
        }
        else if (selectedConstruction is IHiderConstruction hiderConstruction)
        {
            ShowBuildingUnitsPanel(hiderConstruction);
        }
        else
        {
            HideBothPanels();
        }
    }

    private void ShowSelectedUnitsPanel(IReadOnlyList<UnitBase> selectedUnits)
    {
        panel.SetActive(true);
        unitSelectionUI.ShowIcons(selectedUnits);
        buildingUnitsUI.HideIcons();
    }

    private void ShowBuildingUnitsPanel(IHiderConstruction hiderConstruction)
    {
        panel.SetActive(true);
        unitSelectionUI.HideIcons();
        buildingUnitsUI.ShowIcons(hiderConstruction);
    }

    private void HideBothPanels()
    {
        panel.SetActive(false);
        unitSelectionUI.HideIcons();
        buildingUnitsUI.HideIcons();
    }
}