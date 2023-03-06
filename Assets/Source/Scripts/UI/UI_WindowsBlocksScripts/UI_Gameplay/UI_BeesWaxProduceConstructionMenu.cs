using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;

public class UI_BeesWaxProduceConstructionMenu : UIScreen
{
    [SerializeField] private UI_ERROR uiError;
    BeesWaxProduceConstruction _beesWaxProduceConstruction;

    [SerializeField] private Image spendableResourceFill;
    [SerializeField] private TextMeshProUGUI spendableResourceText;
    [SerializeField] private Image produceResourceFill;
    [SerializeField] private TextMeshProUGUI produceResourceText;
    
    private void Update()
    {
        Displaying();
    }

    private void Displaying()
    {
        IReadOnlyResourceStorage spendableResource = _beesWaxProduceConstruction.TakeSpendableResourceInformation();
        spendableResourceFill.fillAmount = spendableResource.CurrentValue / spendableResource.Capacity;
        spendableResourceText.text = spendableResource.CurrentValue + "/" + spendableResource.Capacity;
        
        IReadOnlyResourceStorage produceResource = _beesWaxProduceConstruction.TakeProduceResourceInformation();
        produceResourceFill.fillAmount = produceResource.CurrentValue / produceResource.Capacity;
        produceResourceText.text = produceResource.CurrentValue + "/" + produceResource.Capacity;
    }
    
    public void _CallMenu(GameObject beesWaxProduceConstruction)
    {
        _beesWaxProduceConstruction = beesWaxProduceConstruction.GetComponent<BeesWaxProduceConstruction>();
    }
    
    public void _BuildingLVL_Up()
    {
        _beesWaxProduceConstruction.NextBuildingLevel();
    }
    
    public void _AddSpendableResource(int addPollen)
    {
        _beesWaxProduceConstruction.AddSpendableResource(addPollen);
    }
    
    public void _ExtractProduceResource()
    {
        _beesWaxProduceConstruction.ExtractProduceResource();
    }
}
