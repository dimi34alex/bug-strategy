using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconPanelParent;
    [SerializeField] private UIUnitsConfig uiUnitsConfig;
    [SerializeField] private int maxIcons = 15;

    private readonly List<GameObject> _iconPool = new();

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
    }

    private void InitializeIconPool()
    {
        for (int i = 0; i < maxIcons; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconPanelParent);
            icon.SetActive(false);
            _iconPool.Add(icon);
        }
    }
    
    public void ShowIcons(IReadOnlyList<UnitBase> selectedUnits)
    {
        UpdateUnitIcons(selectedUnits);
    }

    public void HideIcons()
    {
        foreach (GameObject icon in _iconPool)
        {
            icon.SetActive(false);
        }
    }

    private void UpdateUnitIcons(IReadOnlyList<UnitBase> selectedUnits)
    {
        foreach (GameObject icon in _iconPool)
        {
            icon.SetActive(false);
        }

        for (int i = 0; i < Mathf.Min(selectedUnits.Count, maxIcons); i++)
        {
            UnitBase unit = selectedUnits[i];

            if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
            {
                GameObject icon = _iconPool[i];
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
}