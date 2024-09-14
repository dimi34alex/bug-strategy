using System;
using System.Collections.Generic;
using BugStrategy.Missions;
using BugStrategy.ResourcesSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace BugStrategy.UI.Elements
{
    public class ResourcesView : MonoBehaviour
    {
        [Inject] private MissionData _missionData;
        
        [Serializable]
        private struct SomeResourcePrint
        {
            public TextMeshProUGUI name;
            public Image Icon;
            public TextMeshProUGUI value;
        }
    
        [SerializeField] private SomeResourcePrint _pollen;
        [SerializeField] private SomeResourcePrint _wax;
        [SerializeField] private SomeResourcePrint _housing;
        [SerializeField] private SomeResourcePrint _honey;

        private Dictionary<ResourceID, SomeResourcePrint> _printResoucesWithType;

        private void Start()
        {
            var resourceRepository = _missionData.TeamsResourcesGlobalStorage
                    .GetAffiliationResourceRepository(_missionData.PlayerAffiliation);
            
            _printResoucesWithType = new Dictionary<ResourceID, SomeResourcePrint>();

            _printResoucesWithType.Add(ResourceID.Pollen, _pollen);
            _printResoucesWithType.Add(ResourceID.BeesWax, _wax);
            _printResoucesWithType.Add(ResourceID.Housing, _housing);
            _printResoucesWithType.Add(ResourceID.Honey, _honey);

            foreach (var printResource in _printResoucesWithType)
            {
                var resource =  resourceRepository.GetResource(printResource.Key);
                printResource.Value.Icon.sprite = resource.Icon;
                printResource.Value.name.text = resource.ID.ToString();
            }

            resourceRepository.ResourceChanged += UpdateResourceInformation;
            UpdateResourceInformation();
        }
    
        private void UpdateResourceInformation()
        {
            var resourceRepository = _missionData.TeamsResourcesGlobalStorage
                    .GetAffiliationResourceRepository(_missionData.PlayerAffiliation);
            
            foreach (var printResource in _printResoucesWithType)
            {
                var resource = resourceRepository.GetResource(printResource.Key);
                printResource.Value.value.text = $"{resource.CurrentValue} / {resource.Capacity}";
            }
        }
    }
}
