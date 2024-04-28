using System;
using CustomTimer;
using UnityEngine;

namespace Unit.ProcessorsCore
{
    public class ResourceExtractionProcessor
    {
        public readonly int ExtractionCapacity;

        private readonly IAffiliation _affiliation;
        private readonly Timer _extractionTimer;
        private readonly GameObject _resourceSkin;
        private readonly IResourceGlobalStorage _resourceGlobalStorage;
        
        public ResourceID ExtractedResourceID { get; private set; }
        public bool GotResource { get; private set; } = false;
        public bool Extraction { get; private set; } = false;
        
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
        public void StartExtraction(ResourceID resourceID)
        {
            if(Extraction) return;
            
            Extraction = true;
            ExtractedResourceID = resourceID;
            _extractionTimer.Reset();
        }

        /// <summary>
        /// Abort resource extraction timer
        /// </summary>
        public void AbortExtraction()
        {
            if(!Extraction) return;
            
            Extraction = false;
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
            Extraction = false;
            HideResource();
        }
        
        private void ShowResource() => _resourceSkin.SetActive(true);
        private void HideResource() => _resourceSkin.SetActive(false);
        
        private void ExtractResource()
        {
            GotResource = true;
            Extraction = false;
            ShowResource();
            OnResourceExtracted?.Invoke();
        }
    }
}