using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class UI_GameplayMain : UIScreen
{
    [Serializable]
    private struct SomeResourcePrint
    {
        public TextMeshProUGUI name;
        public Image Icon;
        public TextMeshProUGUI value;
    }
    
    [field: SerializeField] public GameObject MiniMapIconZone { get; private set; }

    [SerializeField] private SomeResourcePrint pollen;
    [SerializeField] private SomeResourcePrint wax;
    [SerializeField] private SomeResourcePrint housing;
    [SerializeField] private SomeResourcePrint honey;

    private MiniMapTriggerData _miniMapTriggerData;
    private Transform _cameraTransform;
    private float MiniMapScale;
    
    private void Start()
    {
        MiniMapScale = MiniMapIconZone.transform.localScale.x;
        
        _miniMapTriggerData = MiniMapTriggerData.Instance;
        _cameraTransform = Camera.main.transform;
            
        pollen.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Pollen).Icon;
        pollen.name.text = ResourceGlobalStorage.GetResource(ResourceID.Pollen).ID.ToString();
        
        wax.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).Icon;
        wax.name.text = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).ID.ToString();
        
        housing.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Housing).Icon;
        housing.name.text = ResourceGlobalStorage.GetResource(ResourceID.Housing).ID.ToString();

        honey.Icon.sprite = ResourceGlobalStorage.GetResource(ResourceID.Honey).Icon;
        honey.name.text = ResourceGlobalStorage.GetResource(ResourceID.Honey).ID.ToString();
    }

    private void Update()
    {
        UpdateResourceInformation();
        UpdateMiniMap();
    }

    private void UpdateMiniMap()
    {
        Dictionary<Transform, MiniMapIconBase> miniMapIcons = _miniMapTriggerData.MiniMapIcons;
        
        foreach (var icon in miniMapIcons)
        {
            Vector3 iconPosition = icon.Key.transform.position;
            iconPosition.y = 0;
            
            Vector3 cameraPos = _cameraTransform.position;
            cameraPos.y = 0;
            
            icon.Value.transform.localPosition = iconPosition - cameraPos;
        }
    }

    private void UpdateResourceInformation()
    {
        pollen.value.text = ResourceGlobalStorage.GetResource(ResourceID.Pollen).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Pollen).Capacity.ToString();
        wax.value.text = ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Bees_Wax).Capacity.ToString();
        housing.value.text = ResourceGlobalStorage.GetResource(ResourceID.Housing).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Housing).Capacity.ToString();
        honey.value.text = ResourceGlobalStorage.GetResource(ResourceID.Honey).CurrentValue.ToString() + "/" + ResourceGlobalStorage.GetResource(ResourceID.Honey).Capacity.ToString();
    }
}