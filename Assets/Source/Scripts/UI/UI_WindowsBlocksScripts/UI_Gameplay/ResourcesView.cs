using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class ResourcesView : UIScreen
{
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
        _printResoucesWithType = new Dictionary<ResourceID, SomeResourcePrint>();

        _printResoucesWithType.Add(ResourceID.Pollen, _pollen);
        _printResoucesWithType.Add(ResourceID.Bees_Wax, _wax);
        _printResoucesWithType.Add(ResourceID.Housing, _housing);
        _printResoucesWithType.Add(ResourceID.Honey, _honey);

        foreach (var printResouce in _printResoucesWithType)
        {
            ResourceBase resource = ResourceGlobalStorage.GetResource(printResouce.Key);
            printResouce.Value.Icon.sprite = resource.Icon;
            printResouce.Value.name.text = resource.ID.ToString();
        }

        ResourceGlobalStorage.ResourceChanged += UpdateResourceInformation;
    }
    
    private void UpdateResourceInformation()
    {
        if (_printResoucesWithType!= null) 
        foreach (var printResouce in _printResoucesWithType)
        {
            ResourceBase resource = ResourceGlobalStorage.GetResource(printResouce.Key);
            printResouce.Value.value.text = resource.CurrentValue.ToString() + "/" +
                resource.Capacity.ToString();
        }
    }
}
