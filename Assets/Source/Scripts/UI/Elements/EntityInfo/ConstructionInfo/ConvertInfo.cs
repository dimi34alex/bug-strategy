using System;
using BugStrategy.Bars;
using BugStrategy.Constructions.ResourceProduceConstruction;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    public class ConvertInfo : MonoBehaviour
    {
        [SerializeField] private Composite produceView;
        [SerializeField] private Composite spendView;
        [Space]
        [SerializeField] private Button addButton;
        [SerializeField] private Button extractButton;
        [SerializeField] private Button backButton;
        
        private ResourceConversionConstructionBase _conversionConstruction;
        
        public event Action BackButtonClicked;

        private void Awake()
        {
            addButton.onClick.AddListener(Add);
            extractButton.onClick.AddListener(Extract);
            backButton.onClick.AddListener(() => BackButtonClicked?.Invoke());
        }

        public void Show(bool showBackButton, ResourceConversionConstructionBase conversionConstruction)
        {
            backButton.gameObject.SetActive(showBackButton);

            if (_conversionConstruction != null)
            {
                _conversionConstruction.ProducedResource.Changed -= UpdateProducedResourceBar;
                _conversionConstruction.SpendableResource.Changed -= UpdateSpendableResourceBar;
            }

            _conversionConstruction = conversionConstruction;
            _conversionConstruction.ProducedResource.Changed += UpdateProducedResourceBar;
            _conversionConstruction.SpendableResource.Changed += UpdateSpendableResourceBar;
            UpdateProducedResourceBar();
            UpdateSpendableResourceBar();
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (_conversionConstruction != null)
            {
                _conversionConstruction.ProducedResource.Changed -= UpdateProducedResourceBar;
                _conversionConstruction.SpendableResource.Changed -= UpdateSpendableResourceBar;
            }
            gameObject.SetActive(false);
        }
        
        private void UpdateProducedResourceBar() 
            => produceView.SetStorage(_conversionConstruction.ProducedResource);

        private void UpdateSpendableResourceBar() 
            => spendView.SetStorage(_conversionConstruction.SpendableResource);

        private void Add() 
            => _conversionConstruction.AddSpendableResource(100);
        
        private void Extract() 
            => _conversionConstruction.ExtractProduceResource();
        
        [Serializable]
        private struct Composite
        {
            [SerializeField] private BarView barView;
            [SerializeField] private FloatStorageTextView textView;

            public void SetStorage(IReadOnlyFloatStorage storage)
            {
                barView.SetStorage(storage);
                textView.SetStorage(storage);
            }
        }
    }
}