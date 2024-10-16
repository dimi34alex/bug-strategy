using UnityEngine;
using UnityEngine.UI;
using BugStrategy.Constructions;
using Zenject;
using BugStrategy.Missions;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject mainUIPanel; // �������� UI-������
    [SerializeField] private GameObject hideUIPanel; // ������, ������� ���������� ���������� ������
    [SerializeField] private GameObject pressBPanel; // ������, ����������� �� ������������� ������ ������� "B"

    [Inject] private MissionData _missionData;

    private bool isPressBPressed = false; // ����

    private void Start()
    {
        // ���������, ��� �� ������ ��������� � ����������, � ������� ������, ���� �����-�� �� ��� �� ���������
        if (mainUIPanel == null || hideUIPanel == null || pressBPanel == null)
        {
            Debug.LogError("One or more UI panels are not assigned in the inspector.");
            return;
        }

        // ���������� ���������� �������� ������ � �������� ���������
        mainUIPanel.SetActive(true);
        hideUIPanel.SetActive(false);
        pressBPanel.SetActive(false);

        // ������������� �� ������� ��������� ������ ������, ���� ��� ����������
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange += HandleConstructionSelection;
        }
        else
        {
            Debug.LogError("ConstructionSelector not found in MissionData."); // ������� ��������� �� ������
        }
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� �������
        if (_missionData.ConstructionSelector != null)
        {
            _missionData.ConstructionSelector.OnSelectionChange -= HandleConstructionSelection;
        }
    }

    private void Update()
    {
        // ���������, ������ �� ������� "B" � ������� �� ������ ��� ������������ �������
        if (Input.GetKeyDown(KeyCode.B) && _missionData.ConstructionSelector.SelectedConstruction != null)
        {
            TogglePanels(); // �������� ����� ��� ������������ �������
            isPressBPressed = true; // ��������� ����, ��������, ��� "B" ���� ������
        }
    }

    private void HandleConstructionSelection()
    {
        // ���������, ������� �� ������
        bool isConstructionSelected = _missionData.ConstructionSelector.SelectedConstruction != null;

        // ���������� ������ "Press B", ���� ������ �������, ����� �������� �
        if (isConstructionSelected)
        {
            pressBPanel.SetActive(true);
        }
        else
        {
            // ���� ��������� �����, �������� ������ "Press B" � ���������� ���� isPressBPressed
            pressBPanel.SetActive(false);
            isPressBPressed = false;
        }
    }

    private void TogglePanels()
    {
        // ����������� ���������� �������� � ���������� �������
        mainUIPanel.SetActive(!mainUIPanel.activeSelf);
        hideUIPanel.SetActive(!hideUIPanel.activeSelf);

        // ���� ������ ������ �� �������, �������� ������ "Press B"
        if (_missionData.ConstructionSelector.SelectedConstruction == null)
        {
            pressBPanel.SetActive(false);
        }
    }
}