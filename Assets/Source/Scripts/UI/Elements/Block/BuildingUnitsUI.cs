using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.UnitsHideCore;

public class BuildingUnitsUI : MonoBehaviour
{
    [SerializeField] private UpdateUnit updateUnit;

    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconPanelParent;
    [SerializeField] private UIUnitsConfig uiUnitsConfig;
    [SerializeField] private int maxIcons = 5;

    private readonly List<GameObject> _iconPool = new();
    private IHiderConstruction _currentHiderConstruction;

    private void Start()
    {
        updateUnit = GetComponent<UpdateUnit>();
        InitializeIconPool();
    }

    private void InitializeIconPool()
    {
        for (int i = 0; i < maxIcons; i++)
        {
            GameObject iconInstance = Instantiate(iconPrefab, iconPanelParent);
            Button iconButton = iconInstance.AddComponent<Button>();
            int index = i;
            iconButton.onClick.AddListener(() => ExtractUnit(index));

            iconInstance.SetActive(false);
            _iconPool.Add(iconInstance);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject iconInstance in _iconPool)
        {
            Button button = iconInstance.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
            }
        }
    }
    
    public void ShowIcons(IHiderConstruction selectedConstruction)
    {
        UpdateBuildingUnitIcons(selectedConstruction);
    }

    public void HideIcons()
    {
        foreach (GameObject icon in _iconPool)
        {
            icon.SetActive(false);
        }
    }

    private void UpdateBuildingUnitIcons(IHiderConstruction selectedConstruction)
    {
        foreach (GameObject icon in _iconPool)
        {
            icon.SetActive(false);
        }

        if (selectedConstruction == null)
        {
            _currentHiderConstruction = null;
            return;
        }

        _currentHiderConstruction = selectedConstruction;
        IReadOnlyList<HiderCellBase> hiddenUnits = _currentHiderConstruction.Hider.HiderCells;

        for (int i = 0; i < Mathf.Min(hiddenUnits.Count, maxIcons); i++)
        {
            UpdateUnitIcon(hiddenUnits[i], _iconPool[i]);
        }
    }

    private void UpdateUnitIcon(HiderCellBase unit, GameObject iconInstance)
    {
        if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
        {
            Image[] images = iconInstance.GetComponentsInChildren<Image>();
            if (images.Length >= 2)
            {
                images[1].sprite = unitConfig.InfoSprite;
                iconInstance.SetActive(true);
            }
            else
            {
                Debug.LogError("Недостаточно компонентов Image в префабе иконки.");
            }
        }
        else
        {
            Debug.LogWarning($"Не найдена конфигурация UI для типа юнита: {unit.UnitType}");
        }
    }

    private void ExtractUnit(int index)
    {
        if (_currentHiderConstruction?.Hider == null ||
            _currentHiderConstruction.Hider.HiderCells.Count <= index)
        {
            Debug.LogWarning("Нет доступного юнита для извлечения по указанному индексу.");
            return;
        }

        Vector3 extractPosition = GetRandomExtractPosition(_currentHiderConstruction);

        UnitBase extractedUnit;

        if(!updateUnit.GetActiveFunc()) 
            extractedUnit = _currentHiderConstruction.Hider.ExtractUnit(index, extractPosition);
        else
            extractedUnit = _currentHiderConstruction.Hider.ExtractUpgradeUnit(index, extractPosition);
        
        if (extractedUnit != null)
        {
            Debug.Log($"Юнит {extractedUnit.UnitType} извлечен из здания.");
        }
        else
        {
            Debug.LogWarning("Не удалось извлечь юнита.");
        }

        UpdateBuildingUnitIcons(_currentHiderConstruction);
    }

    private static Vector3 GetRandomExtractPosition(IHiderConstruction hiderConstruction)
    {
        Vector3 buildingPosition = ((MonoBehaviour)hiderConstruction).transform.position;
        float radius = 2f;
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

        return buildingPosition + new Vector3(
            Mathf.Cos(angle) * radius,
            0,
            Mathf.Sin(angle) * radius
        );
    }
}