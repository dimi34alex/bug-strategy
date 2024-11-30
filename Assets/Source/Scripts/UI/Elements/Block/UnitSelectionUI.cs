using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.Unit.UnitSelection;
using Zenject;
using System;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconPanelParent;
    [SerializeField] private UIUnitsConfig uiUnitsConfig;
    [SerializeField] private int maxIcons = 15;

    [Inject] private UnitsSelector _unitsSelector;
    private List<GameObject> iconPool = new List<GameObject>();

    private void Awake()
    {
        if (iconPrefab == null)
        {
            Debug.LogError("Префаба иконки нет");
            return;
        }

        if (iconPanelParent == null)
        {
            Debug.LogError("Родительский объект панели иконок не назначен.");
            return;
        }

        if (uiUnitsConfig == null)
        {
            Debug.LogError("Конфига UI юнитов нет.");
            return;
        }

        InitializeIconPool();
        _unitsSelector.OnSelectionChanged += UpdateUnitIcons;
    }

    private void OnDestroy()
    {
        _unitsSelector.OnSelectionChanged -= UpdateUnitIcons;
    }

    private void InitializeIconPool()
    {
        for (int i = 0; i < maxIcons; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconPanelParent);
            icon.SetActive(false);
            iconPool.Add(icon);
        }
    }

    private void UpdateUnitIcons()
    {
        IReadOnlyList<UnitBase> selectedUnits = _unitsSelector.GetSelectedUnits();

        foreach (GameObject icon in iconPool)
        {
            icon.SetActive(false);
        }

        for (int i = 0; i < Mathf.Min(selectedUnits.Count, maxIcons); i++)
        {
            UnitBase unit = selectedUnits[i];

            if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
            {
                GameObject icon = iconPool[i];
                icon.SetActive(true);

                Image[] images = icon.GetComponentsInChildren<Image>();
                if (images.Length >= 2)
                {
                    images[1].sprite = unitConfig.InfoSprite;
                }
                else
                {
                    Debug.LogError("Недостаточно компонентов Image в префабе иконки. Должно быть 2.");
                }
            }
        }
    }

    public void ShowIcons()
    {
        UpdateUnitIcons();
    }

    public void HideIcons()
    {
        foreach (GameObject icon in iconPool)
        {
            icon.SetActive(false);
        }
    }
}