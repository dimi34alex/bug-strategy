using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
[Serializable]
struct SomeResurcePrint
{
    public TextMeshProUGUI name;
    public Image Icon;
    public TextMeshProUGUI value;
    public int CurrentValue;
    public int Capacity;
}

public class UI_GameplayMain : UIScreen
{
    [SerializeField] private SomeResurcePrint pollen;
    [SerializeField] private SomeResurcePrint wax;
    [SerializeField] private SomeResurcePrint housing;

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
        
        pollen.Capacity = 1000;
        pollen.CurrentValue = 0;
    }

    private void Update()
    {
        pollen.value.text = ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Pollen).Capacity.ToString();
        wax.value.text = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).Capacity.ToString();
        housing.value.text = ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Housing).Capacity.ToString();
        honey.value.text = ResourceGlobalStorage.GetResource(ResourceID.Honey).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Honey).Capacity.ToString();
    }

    public void GatheringPollen(int count)
    {
        ResourceGlobalStorage.ChangeValue(ResourceID.Pollen, count);
    }
}
