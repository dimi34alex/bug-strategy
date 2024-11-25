using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.Unit.UnitSelection;
using Zenject;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform iconPanelParent;
    [SerializeField] private UIUnitsConfig uiUnitsConfig;

    [SerializeField] private int maxIcons = 15; // ������������ ���������� ������ (��������, 3 ���� �� 5)

    [Inject] private UnitsSelector _unitsSelector;
    private List<GameObject> iconPool = new List<GameObject>();

    private void Awake()
    {
        if (iconPrefab == null)
        {
            Debug.LogError("Icon prefab is not assigned.");
            return;
        }

        if (iconPanelParent == null)
        {
            Debug.LogError("Icon panel parent is not assigned.");
            return;
        }

        if (uiUnitsConfig == null)
        {
            Debug.LogError("UI Units Config is not assigned.");
            return;
        }

        // ������������� ���� ������
        InitializeIconPool();
    }

    private void InitializeIconPool()
    {
        // ������� ������������ ���������� ������ � ������������ ��
        for (int i = 0; i < maxIcons; i++)
        {
            GameObject icon = Instantiate(iconPrefab, iconPanelParent);
            icon.SetActive(false);
            iconPool.Add(icon);
        }
    }

    private void Update()
    {
        if (_unitsSelector != null)
        {
            IReadOnlyList<UnitBase> selectedUnits = _unitsSelector.GetSelectedUnits();
            UpdateUnitIcons(selectedUnits);
        }
    }

    private void UpdateUnitIcons(IReadOnlyList<UnitBase> selectedUnits)
    {
        // ������������ ��� ������
        foreach (GameObject icon in iconPool)
        {
            icon.SetActive(false);
        }

        // ���������� � ��������� ������ ���������� ������
        for (int i = 0; i < Mathf.Min(selectedUnits.Count, maxIcons); i++)
        {
            UnitBase unit = selectedUnits[i];

            if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
            {
                GameObject icon = iconPool[i];
                icon.SetActive(true);

                // ������������� ������ ��� �����
                Image[] images = icon.GetComponentsInChildren<Image>();
                if (images.Length >= 2)
                {
                    images[1].sprite = unitConfig.InfoSprite;
                }
                else
                {
                    Debug.LogError("Not enough Image components found in icon prefab. Expected at least 2.");
                }
            }
        }
    }
}