using System;
using Constructions;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using TMPro;

public class UI_BeesWaxProduceConstructionMenu : UI_EvolveConstructionScreenBase<BeesWaxProduceConstruction>
{
    [SerializeField] private FillBar spendableResource;
    [SerializeField] private FillBar produceResource;
    
    public void _CallMenu(ConstructionBase beesWaxProduceConstruction)
    {
        _construction = beesWaxProduceConstruction.Cast<BeesWaxProduceConstruction>();
        
        spendableResource.SetResourceStorage(_construction.TakeSpendableResourceInformation());
        produceResource.SetResourceStorage(_construction.TakeProduceResourceInformation());
    }
    
    public void _AddSpendableResource(int addPollen)
    {
        _construction.AddSpendableResource(addPollen);
    }
    
    public void _ExtractProduceResource()
    {
        _construction.ExtractProduceResource();
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
            _resourceStorage.Changed -= UpdateInfo;
            
            _resourceStorage = newIReadOnlyResourceStorage;
            _resourceStorage.Changed += UpdateInfo;

            UpdateInfo();
        }

        public void Reset()
        {
            _resourceStorage.Changed -= UpdateInfo;
        }
        
        private void UpdateInfo()
        {
            ImageFill.fillAmount = _resourceStorage.CurrentValue / _resourceStorage.Capacity;
            Text.text = _resourceStorage.CurrentValue + "/" + _resourceStorage.Capacity;
        }

        ~FillBar()
        {
            if (_resourceStorage != null)
                _resourceStorage.Changed -= UpdateInfo;
        }
    }
}
