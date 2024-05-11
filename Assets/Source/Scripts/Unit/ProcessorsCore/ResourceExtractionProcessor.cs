using System;
using CustomTimer;
using UnityEngine;

namespace Unit.ProcessorsCore
{
    public class ResourceExtractionProcessor : IReadOnlyResourceExtractionProcessor
    {
        private readonly IAffiliation _affiliation;
        private readonly Timer _extractionTimer;
        private readonly GameObject _resourceSkin;
        private readonly IResourceGlobalStorage _resourceGlobalStorage;
        private ResourceSourceBase _prevResourceSource;
        
        public ResourceID ExtractedResourceID { get; private set; }
        public bool GotResource { get; private set; } = false;
        public bool IsExtract { get; private set; } = false;
        public int ExtractionCapacity { get; }
        public ResourceSourceBase PrevResourceSource => _prevResourceSource;
        
        public event Action OnResourceExtracted;
        public event Action OnStorageResources;
        
        public ResourceExtractionProcessor(IAffiliation affiliation, int gatheringCapacity, float extractionTime, IResourceGlobalStorage resourceGlobalStorage, GameObject resourceSkin)
        {
            _affiliation = affiliation;
            ExtractionCapacity = gatheringCapacity;
            _extractionTimer = new Timer(extractionTime, 0, true);
            _extractionTimer.OnTimerEnd += ExtractResource;
            _resourceGlobalStorage = resourceGlobalStorage;
            _resourceSkin = resourceSkin;
            HideResource();
        }
        
        public void HandleUpdate(float time) => _extractionTimer.Tick(time);

        public void LoadData(bool resourceExtracted, ResourceID extractedResourceID)
        {
            if (resourceExtracted)
            {
                ExtractedResourceID = extractedResourceID;
                ExtractResource();              
            }
        }
        
        /// <summary>
        /// Start resource extraction timer
        /// </summary>
        public void StartExtraction(ResourceSourceBase resourceSource)
        {
            if(IsExtract) return;

            _prevResourceSource = resourceSource;
            IsExtract = true;
            ExtractedResourceID = _prevResourceSource.ResourceID;
            _extractionTimer.Reset();
        }

        /// <summary>
        /// Abort resource extraction timer
        /// </summary>
        public void AbortExtraction()
        {
            if(!IsExtract) return;
            
            IsExtract = false;
            _extractionTimer.Reset(true);
        }
        
        /// <summary>
        /// Give order put resource in storage
        /// </summary>
        public void StorageResources()
        {
            if(!GotResource) return;
            
            _resourceGlobalStorage.ChangeValue(_affiliation.Affiliation, ExtractedResourceID, ExtractionCapacity);
            GotResource = false;
            HideResource();
            
            OnStorageResources?.Invoke();
        }

        public void Reset()
        {
            _extractionTimer.Reset(true);
            _extractionTimer.OnTimerEnd += ExtractResource;
            GotResource = false;
            IsExtract = false;
            HideResource();
        }
        
        private void ShowResource() => _resourceSkin.SetActive(true);
        private void HideResource() => _resourceSkin.SetActive(false);
        
        private void ExtractResource()
        {
            if (_prevResourceSource.CanBeCollected)
            {
                GotResource = true;
                ShowResource();
                _prevResourceSource.ExtractResource(ExtractionCapacity);    
            }
            
            IsExtract = false;
            
            OnResourceExtracted?.Invoke();
        }
    }
}