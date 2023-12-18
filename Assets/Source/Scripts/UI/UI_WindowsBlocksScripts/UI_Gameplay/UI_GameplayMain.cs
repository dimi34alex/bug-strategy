using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class UI_GameplayMain : UIScreen
{
    [Serializable]
    private struct SomeResourcePrint
    {
        public TextMeshProUGUI name;
        public Image Icon;
        public TextMeshProUGUI value;
    }
    
    [SerializeField] private SomeResourcePrint pollen;
    [SerializeField] private SomeResourcePrint wax;
    [SerializeField] private SomeResourcePrint housing;
    [SerializeField] private SomeResourcePrint honey;

    private Dictionary<ResourceID, SomeResourcePrint> _printResoucesWithType;

    private void Awake()
    {
        _printResoucesWithType = new Dictionary<ResourceID, SomeResourcePrint>();

        _printResoucesWithType.Add(ResourceID.Pollen, pollen);
        _printResoucesWithType.Add(ResourceID.Bees_Wax, wax);
        _printResoucesWithType.Add(ResourceID.Housing, housing);
        _printResoucesWithType.Add(ResourceID.Honey, honey);

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
