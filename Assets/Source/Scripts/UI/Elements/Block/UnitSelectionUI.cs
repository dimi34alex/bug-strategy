using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.Unit.UnitSelection;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab; // ������ ������ ��� ����������� �����
    [SerializeField] private Transform iconPanelParent; // ������������ ������ ��� ������
    [SerializeField] private UIUnitsConfig uiUnitsConfig; // ������������ UI ��� ����������� ������ ������

    [SerializeField] private int maxRows = 3; // �������� ����� ��� ����������� ������
    [SerializeField] private float iconSpacingPercentage = 0.02f; // ������� �� ������ �������� ��� ���������� ����� ��������
    [SerializeField] private float topOffsetPercentage = 0.05f; // ������� �� ������ �������� ��� ������� ������
    [SerializeField] private float leftOffsetPercentage = 0.05f; // ������� �� ������ �������� ��� ������� �����

    private UnitSelection _unitSelection; // ��������� ��� ������ ������
    private List<GameObject> activeIcons = new List<GameObject>(); // ������ �������� ������

    private void Awake()
    {
        // ����� ���������� ������ ������
        _unitSelection = FindObjectOfType<UnitSelection>();

        // ���������, ��������� �� ����������� ����������
        if (_unitSelection == null)
        {
            Debug.LogError("UnitSelection component not found."); // ���, ���� ��������� �� ������
        }

        if (iconPrefab == null)
        {
            Debug.LogError("Icon prefab is not assigned."); // ���, ���� ������ �� ��������
        }

        if (iconPanelParent == null)
        {
            Debug.LogError("Icon panel parent is not assigned."); // ���, ���� �������� �� ��������
        }

        if (uiUnitsConfig == null)
        {
            Debug.LogError("UI Units Config is not assigned."); // ���, ���� ������ �� ��������
        }
    }

    private void Start()
    {
        // ��������� ������ ������ ��� ������
        UpdateUnitIcons(_unitSelection.GetSelectedUnits());
    }

    private void Update()
    {
        if (_unitSelection != null)
        {
            // ��������� ������ ������ ��� ��������� ������
            List<UnitBase> selectedUnits = _unitSelection.GetSelectedUnits();
            UpdateUnitIcons(selectedUnits);
        }
    }

    // ���������� ������ ������ � UI
    private void UpdateUnitIcons(List<UnitBase> selectedUnits)
    {
        // ������� ���������� ������
        foreach (GameObject icon in activeIcons)
        {
            Destroy(icon);
        }
        activeIcons.Clear();

        // ������������ ������� � ���������� �� ������ �������� ������������� ����������
        RectTransform parentRect = iconPanelParent.GetComponent<RectTransform>();
        float parentWidth = parentRect.rect.width;
        float parentHeight = parentRect.rect.height;

        float iconSpacing = iconSpacingPercentage * parentWidth;
        float topOffset = topOffsetPercentage * parentHeight;
        float leftOffset = leftOffsetPercentage * parentWidth;

        // �������� ������ � ������ ������
        RectTransform iconPrefabRect = iconPrefab.GetComponent<RectTransform>();
        float iconWidth = iconPrefabRect.sizeDelta.x;
        float iconHeight = iconPrefabRect.sizeDelta.y;

        // ������������ ������������ ���������� ������ � ����� ���� �� ������ ������ ������������� ����������
        int maxIconsPerRow = Mathf.FloorToInt((parentWidth - leftOffset) / (iconWidth + iconSpacing));

        int totalIcons = 0; // ������� ������
        int rowCount = 0; // ������� �����

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (totalIcons >= maxIconsPerRow * maxRows)
            {
                Debug.LogWarning("Selected maximum number of units for display."); // ��� �����, ���� ������ �������� ������
                return;
            }

            UnitBase unit = selectedUnits[i];

            if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
            {
                GameObject newIcon = Instantiate(iconPrefab, iconPanelParent);

                // ���������� ��������� � ����
                int columnIndex = totalIcons % maxIconsPerRow;
                int rowIndex = totalIcons / maxIconsPerRow;

                // ���������������� ������
                RectTransform iconRect = newIcon.GetComponent<RectTransform>();
                iconRect.anchorMin = new Vector2(0, 1);
                iconRect.anchorMax = new Vector2(0, 1);
                iconRect.pivot = new Vector2(0, 1);

                // ������������� ���������� ��������� ������ � ������ �������� � ������� ������
                float xPos = leftOffset + columnIndex * (iconWidth + iconSpacing);
                float yPos = -topOffset - rowIndex * (iconHeight + iconSpacing);
                iconRect.anchoredPosition = new Vector2(xPos, yPos);

                // ������������� ������ ��� �����
                Image[] images = newIcon.GetComponentsInChildren<Image>();
                if (images.Length >= 2)
                {
                    images[1].sprite = unitConfig.InfoSprite;
                    // Debug.Log($"Created icon for unit type: {unit.UnitType}"); // ��� ��� ������������ ������, ���� �����������
                }
                else
                {
                    Debug.LogError("Not enough Image components found in icon prefab. Expected at least 2."); // ���, ���� �� ������� Image �����������
                }

                activeIcons.Add(newIcon);
                totalIcons++;
            }
            else
            {
                // Debug.LogWarning($"No UI config found for unit type: {unit.UnitType}"); // ��� ��� ������������ ������, ���� �����������
            }
        }
    }
}