using System;
using BugStrategy.CustomTimer;
using BugStrategy.ResourceSources;
using BugStrategy.ResourcesSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.TechnologiesSystem.Technologies;
using UnityEngine;

namespace BugStrategy.Unit.ProcessorsCore
{
    public class ResourceExtractionProcessor : IReadOnlyResourceExtractionProcessor
    {
        private readonly IAffiliation _affiliation;
        private readonly Timer _extractionTimer;
        private readonly GameObject _resourceSkin;
        private readonly ITeamsResourcesGlobalStorage _teamsResourcesGlobalStorage;
        private TechWorkerBeeResourcesExtension _technology;
        
        public ResourceID ExtractedResourceID { get; private set; }
        public bool GotResource { get; private set; } = false;
        public bool IsExtract { get; private set; } = false;
        public ResourceSourceBase LastResourceSource { get; private set; }
        public int ExtractionCapacity => (int)(MainExtractionCapacity * GetResourcesCapacityScale());

        private int MainExtractionCapacity { get; }
        
        public event Action OnResourceExtracted;
        public event Action OnStorageResources;
        
        public ResourceExtractionProcessor(IAffiliation affiliation, int gatheringCapacity, float extractionTime, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage, GameObject resourceSkin)
        {
            _affiliation = affiliation;
            MainExtractionCapacity = gatheringCapacity;
            _extractionTimer = new Timer(extractionTime, 0, true);
            _extractionTimer.OnTimerEnd += ExtractResource;
            _teamsResourcesGlobalStorage = teamsResourcesGlobalStorage;
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

            LastResourceSource = resourceSource;
            IsExtract = true;
            ExtractedResourceID = LastResourceSource.ResourceID;
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
            
            _teamsResourcesGlobalStorage.ChangeValue(_affiliation.Affiliation, ExtractedResourceID, ExtractionCapacity);
            GotResource = false;
            HideResource();
            
            OnStorageResources?.Invoke();
        }

        public void SetTechnology(TechWorkerBeeResourcesExtension tech)
        {
            _technology = tech;
        }
        
        public void Reset()
        {
            _extractionTimer.Reset(true);
            GotResource = false;
            IsExtract = false;
            _technology = null;
            LastResourceSource = null;
            HideResource();
        }
        
        private void ShowResource() => _resourceSkin.SetActive(true);
        private void HideResource() => _resourceSkin.SetActive(false);

        private float GetResourcesCapacityScale() 
            => _technology?.GetCapacityScale() ?? 1;

        private void ExtractResource()
        {
            if (LastResourceSource.CanBeCollected)
            {
                GotResource = true;
                ShowResource();
                LastResourceSource.ExtractResource(ExtractionCapacity);    
            }
            
            IsExtract = false;
            
            OnResourceExtracted?.Invoke();
        }
    }
}