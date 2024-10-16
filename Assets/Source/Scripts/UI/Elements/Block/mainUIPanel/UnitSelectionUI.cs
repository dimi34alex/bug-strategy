using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Unit;
using BugStrategy.UI.Elements.EntityInfo.UnitInfo;
using BugStrategy.Unit.UnitSelection;

public class UnitSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab; // Префаб иконки для отображения юнита
    [SerializeField] private Transform iconPanelParent; // Родительский объект для иконок
    [SerializeField] private UIUnitsConfig uiUnitsConfig; // Конфигурация UI для отображения иконок юнитов

    [SerializeField] private int maxRows = 3; // Максимум рядов для отображения иконок
    [SerializeField] private float iconSpacingPercentage = 0.02f; // Процент от ширины родителя для расстояния между иконками
    [SerializeField] private float topOffsetPercentage = 0.05f; // Процент от высоты родителя для отступа сверху
    [SerializeField] private float leftOffsetPercentage = 0.05f; // Процент от ширины родителя для отступа слева

    private UnitSelection _unitSelection; // Компонент для выбора юнитов
    private List<GameObject> activeIcons = new List<GameObject>(); // Список активных иконок

    private void Awake()
    {
        // Поиск компонента выбора юнитов
        _unitSelection = FindObjectOfType<UnitSelection>();

        // Проверяем, назначены ли необходимые компоненты
        if (_unitSelection == null)
        {
            Debug.LogError("UnitSelection component not found."); // Лог, если компонент не найден
        }

        if (iconPrefab == null)
        {
            Debug.LogError("Icon prefab is not assigned."); // Лог, если префаб не назначен
        }

        if (iconPanelParent == null)
        {
            Debug.LogError("Icon panel parent is not assigned."); // Лог, если родитель не назначен
        }

        if (uiUnitsConfig == null)
        {
            Debug.LogError("UI Units Config is not assigned."); // Лог, если конфиг не назначен
        }
    }

    private void Start()
    {
        // Обновляем иконки юнитов при старте
        UpdateUnitIcons(_unitSelection.GetSelectedUnits());
    }

    private void Update()
    {
        if (_unitSelection != null)
        {
            // Обновляем иконки юнитов при изменении выбора
            List<UnitBase> selectedUnits = _unitSelection.GetSelectedUnits();
            UpdateUnitIcons(selectedUnits);
        }
    }

    // Обновление иконок юнитов в UI
    private void UpdateUnitIcons(List<UnitBase> selectedUnits)
    {
        // Очищаем предыдущие иконки
        foreach (GameObject icon in activeIcons)
        {
            Destroy(icon);
        }
        activeIcons.Clear();

        // Рассчитываем отступы и расстояния на основе размеров родительского компонента
        RectTransform parentRect = iconPanelParent.GetComponent<RectTransform>();
        float parentWidth = parentRect.rect.width;
        float parentHeight = parentRect.rect.height;

        float iconSpacing = iconSpacingPercentage * parentWidth;
        float topOffset = topOffsetPercentage * parentHeight;
        float leftOffset = leftOffsetPercentage * parentWidth;

        // Получаем ширину и высоту иконки
        RectTransform iconPrefabRect = iconPrefab.GetComponent<RectTransform>();
        float iconWidth = iconPrefabRect.sizeDelta.x;
        float iconHeight = iconPrefabRect.sizeDelta.y;

        // Рассчитываем максимальное количество иконок в одном ряду на основе ширины родительского компонента
        int maxIconsPerRow = Mathf.FloorToInt((parentWidth - leftOffset) / (iconWidth + iconSpacing));

        int totalIcons = 0; // Счётчик иконок
        int rowCount = 0; // Счётчик рядов

        for (int i = 0; i < selectedUnits.Count; i++)
        {
            if (totalIcons >= maxIconsPerRow * maxRows)
            {
                Debug.LogWarning("Selected maximum number of units for display."); // Лог важен, если выбран максимум юнитов
                return;
            }

            UnitBase unit = selectedUnits[i];

            if (uiUnitsConfig.UnitsUIConfigs.TryGetValue(unit.UnitType, out UIUnitConfig unitConfig))
            {
                GameObject newIcon = Instantiate(iconPrefab, iconPanelParent);

                // Определяем положение в ряду
                int columnIndex = totalIcons % maxIconsPerRow;
                int rowIndex = totalIcons / maxIconsPerRow;

                // Позиционирование иконки
                RectTransform iconRect = newIcon.GetComponent<RectTransform>();
                iconRect.anchorMin = new Vector2(0, 1);
                iconRect.anchorMax = new Vector2(0, 1);
                iconRect.pivot = new Vector2(0, 1);

                // Устанавливаем адаптивное положение иконки с учётом отступов и размера экрана
                float xPos = leftOffset + columnIndex * (iconWidth + iconSpacing);
                float yPos = -topOffset - rowIndex * (iconHeight + iconSpacing);
                iconRect.anchoredPosition = new Vector2(xPos, yPos);

                // Устанавливаем спрайт для юнита
                Image[] images = newIcon.GetComponentsInChildren<Image>();
                if (images.Length >= 2)
                {
                    images[1].sprite = unitConfig.InfoSprite;
                    // Debug.Log($"Created icon for unit type: {unit.UnitType}"); // Лог для вылавливания ошибок, если понадобится
                }
                else
                {
                    Debug.LogError("Not enough Image components found in icon prefab. Expected at least 2."); // Лог, если не хватает Image компонентов
                }

                activeIcons.Add(newIcon);
                totalIcons++;
            }
            else
            {
                // Debug.LogWarning($"No UI config found for unit type: {unit.UnitType}"); // Лог для вылавливания ошибок, если понадобится
            }
        }
    }
}