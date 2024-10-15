using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Constructions;
using BugStrategy.UI.Elements.EntityInfo.ConstructionInfo;
using BugStrategy.Missions;
using Zenject;

public class ConstructionSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject iconPrefab; // ������ ������ ��� ����������� ���������� ������
    [SerializeField] private Transform iconPanelParent; // ������������ ������ ��� ������
    [SerializeField] private UIConstructionsConfig uiConstructionsConfig; // ������������ UI ��� ����������� ������ ������

    [Inject] private MissionData _missionData; // ���������� � ������ ������
    private GameObject activeIcon; // �������� ������� �������� ������

    private void Awake()
    {
        // ��������, ��������� �� ����������� ����������
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
        // ������������� �� ������� ����� ������ ������
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
        // ������������ �� ������� ��� ����������� �������
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= UpdateConstructionIcon;
        }
    }

    private void UpdateConstructionIcon()
    {
        // ������� ���������� ������, ���� ��� ����
        if (activeIcon != null)
        {
            Destroy(activeIcon);
        }

        // �������� ������� ��������� ������
        ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;
        if (selectedConstruction == null)
        {
            return;
        }

        // ������� ����� ������ ��� ���������� ������
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
        // ������������ ������ ������
        activeIcon = Instantiate(iconPrefab, iconPanelParent);

        // ����������� ������ ������
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