using System;
using Constructions;
using Source.Scripts.ResourcesSystem;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Source.Scripts.UI.UI_WindowsBlocksScripts.UI_Gameplay
{
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
            private IReadOnlyFloatStorage _fillProgressStorage = new FloatStorage(0,0);

            public void SetResourceStorage(IReadOnlyFloatStorage newIReadOnlyResourceStorage)
            {
                _fillProgressStorage.Changed -= UpdateInfo;
            
                _fillProgressStorage = newIReadOnlyResourceStorage;
                _fillProgressStorage.Changed += UpdateInfo;

                UpdateInfo();
            }

            public void Reset()
            {
                _fillProgressStorage.Changed -= UpdateInfo;
            }
        
            private void UpdateInfo()
            {
                ImageFill.fillAmount = _fillProgressStorage.CurrentValue / _fillProgressStorage.Capacity;
                Text.text = _fillProgressStorage.CurrentValue + "/" + _fillProgressStorage.Capacity;
            }

            ~FillBar()
            {
                if (_fillProgressStorage != null)
                    _fillProgressStorage.Changed -= UpdateInfo;
            }
        }
    }
}
