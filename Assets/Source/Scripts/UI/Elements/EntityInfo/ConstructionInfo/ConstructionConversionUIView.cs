using System;
using BugStrategy.Constructions.ResourceProduceConstruction;
using BugStrategy.UI.Elements.FloatStorageViews;
using UnityEngine;
using UnityEngine.UI;

namespace BugStrategy.UI.Elements.EntityInfo.ConstructionInfo
{
    public class ConstructionConversionUIView : MonoBehaviour
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
            
            _conversionConstruction = conversionConstruction;

            produceView.SetStorage(_conversionConstruction.ProducedResource);
            spendView.SetStorage(_conversionConstruction.SpendableResource);
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            produceView.SetStorage(null);
            spendView.SetStorage(null);
            
            gameObject.SetActive(false);
        }
        
        private void Add() 
            => _conversionConstruction.AddSpendableResource(100);
        
        private void Extract() 
            => _conversionConstruction.ExtractProduceResource();
        
        [Serializable]
        private struct Composite
        {
            [SerializeField] private FloatStorageBarView barView;
            [SerializeField] private FloatStorageTextView textView;

            public void SetStorage(IReadOnlyFloatStorage storage)
            {
                barView.SetStorage(storage);
                textView.SetStorage(storage);
            }
        }
    }
}