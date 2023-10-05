using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    private void Start()
    {
        pollen.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Pollen).Icon;
        pollen.name.text = ResourceGlobalStorage.GetResource(ResourceID.Pollen).ID.ToString();
        
        wax.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).Icon;
        wax.name.text = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).ID.ToString();
        
        housing.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Housing).Icon;
        housing.name.text = ResourceGlobalStorage.GetResource(ResourceID.Housing).ID.ToString();

        honey.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Honey).Icon;
        honey.name.text = ResourceGlobalStorage.GetResource(ResourceID.Honey).ID.ToString();
    }

    private void Awake()
    {
        ResourceGlobalStorage.ResourceChanged += UpdateResourceInformation;
    }
    
    private void UpdateResourceInformation()
    {
        pollen.value.text = ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Pollen).Capacity.ToString();
        wax.value.text = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).Capacity.ToString();
        housing.value.text = ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Housing).Capacity.ToString();
        honey.value.text = ResourceGlobalStorage.GetResource(ResourceID.Honey).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Honey).Capacity.ToString();
    }
}
