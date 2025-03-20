using System.Collections.Generic;
using BugStrategy.Constructions;
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
    
    [Inject] private UnitsSelector _unitsSelector;
    [Inject] private ConstructionSelector _constructionSelector;

    private void Awake()
    {
        _unitsSelector.OnSelectionChanged += UpdateUnitPanel;
        _constructionSelector.OnSelectionChange += UpdateUnitPanel;
        
        UpdateUnitPanel();
    }

    private void OnDestroy()
    {
        _unitsSelector.OnSelectionChanged -= UpdateUnitPanel;
        _constructionSelector.OnSelectionChange -= UpdateUnitPanel;
    }

    private void UpdateUnitPanel()
    {
        var selectedUnits = _unitsSelector.GetPlayerSelectedUnits();
        var selectedConstruction = _constructionSelector.SelectedConstruction;

        if (selectedUnits.Count > 0)
        {
            if (selectedConstruction is IHiderConstruction hiderConstruction)
                ShowBuildingUnitsPanel(hiderConstruction);
            else
                ShowSelectedUnitsPanel(selectedUnits);
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