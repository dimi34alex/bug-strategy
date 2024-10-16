using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Constructions;
using BugStrategy.UI.Elements.EntityInfo.ConstructionInfo;
using BugStrategy.Missions;
using Zenject;

public class ConstructionSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab; // Префаб иконки для отображения выбранного здания
    [SerializeField] private Transform iconPanelParent; // Родительский объект для иконок
    [SerializeField] private UIConstructionsConfig uiConstructionsConfig; // Конфигурация UI для отображения иконок зданий

    [Inject] private MissionData _missionData; // информация о выборе зданий
    private GameObject activeIcon; // Хранение текущей активной иконки

    private void Awake()
    {
        // Проверка, назначены ли необходимые компоненты
        if (iconPrefab == null)
        {
            Debug.LogError("Icon prefab is not assigned.");
        }

        if (iconPanelParent == null)
        {
            Debug.LogError("Icon panel parent is not assigned.");
        }

        if (uiConstructionsConfig == null)
        {
            Debug.LogError("UI Constructions Config is not assigned.");
        }
    }

    private void Start()
    {
        // Подписываемся на событие смены выбора здания
        if (_missionData.ConstructionSelector == null)
        {
            Debug.LogError("ConstructionSelector not found in MissionData.");
        }
        else
        {
            _missionData.ConstructionSelector.OnSelectionChange += UpdateConstructionIcon;
        }
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateConstructionIcon;
        }
    }

    private void UpdateConstructionIcon()
    {
        // Удаляем предыдущую иконку, если она есть
        if (activeIcon != null)
        {
            Destroy(activeIcon);
        }

        // Получаем текущее выбранное здание
        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
        if (selectedConstruction == null)
        {
            return;
        }

        // Создаем новую иконку для выбранного здания
        if (uiConstructionsConfig.ConstructionsUIConfigs.TryGetValue(selectedConstruction.ConstructionID, out UIConstructionConfig constructionConfig))
        {
            CreateConstructionIcon(selectedConstruction, constructionConfig);
        }
        else
        {
            Debug.LogWarning($"No UI config found for construction type: {selectedConstruction.ConstructionID}");
        }
    }

    private void CreateConstructionIcon(ConstructionBase construction, UIConstructionConfig config)
    {
        // Инстанцируем префаб иконки
        activeIcon = Instantiate(iconPrefab, iconPanelParent);

        // Настраиваем спрайт иконки
        Image[] images = activeIcon.GetComponentsInChildren<Image>();
        if (images.Length >= 2)
        {
            images[1].sprite = config.InfoSprite;
        }
        else
        {
            Debug.LogError("Not enough Image components found in icon prefab. Expected at least 2.");
        }
    }
}