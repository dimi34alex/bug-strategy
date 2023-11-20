using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using TMPro;

public class UI_BeesWaxProduceConstructionMenu : UIScreen
{
    [SerializeField] private FillBar spendableResource;
    [SerializeField] private FillBar produceResource;

    BeesWaxProduceConstruction _beesWaxProduceConstruction;

    public void _CallMenu(ConstructionBase beesWaxProduceConstruction)
    {
        _beesWaxProduceConstruction = beesWaxProduceConstruction.Cast<BeesWaxProduceConstruction>();
        
        spendableResource.SetResourceStorage(_beesWaxProduceConstruction.TakeSpendableResourceInformation());
        produceResource.SetResourceStorage(_beesWaxProduceConstruction.TakeProduceResourceInformation());
    }
    
    public void _BuildingLVL_Up()
    {
        _beesWaxProduceConstruction.LevelUp();
    }
    
    public void _AddSpendableResource(int addPollen)
    {
        _beesWaxProduceConstruction.AddSpendableResource(addPollen);
    }
    
    public void _ExtractProduceResource()
    {
        _beesWaxProduceConstruction.ExtractProduceResource();
    }

    private void OnDisable()
    {
        spendableResource.Reset();
        produceResource.Reset();
    }

    [Serializable]
    private class FillBar
    {
        [field: SerializeField] public Image ImageFill { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Text { get; private set; }
        private IReadOnlyResourceStorage _resourceStorage = new ResourceStorage(0,0);

        public void SetResourceStorage(IReadOnlyResourceStorage newIReadOnlyResourceStorage)
        {
            _resourceStorage.OnChange -= UpdateInfo;
            
            _resourceStorage = newIReadOnlyResourceStorage;
            _resourceStorage.OnChange += UpdateInfo;

            UpdateInfo();
        }

        public void Reset()
        {
            _resourceStorage.OnChange -= UpdateInfo;
        }
        
        private void UpdateInfo()
        {
            ImageFill.fillAmount = _resourceStorage.CurrentValue / _resourceStorage.Capacity;
            Text.text = _resourceStorage.CurrentValue + "/" + _resourceStorage.Capacity;
        }

        ~FillBar()
        {
            if (_resourceStorage != null)
                _resourceStorage.OnChange -= UpdateInfo;
        }
    }
}
