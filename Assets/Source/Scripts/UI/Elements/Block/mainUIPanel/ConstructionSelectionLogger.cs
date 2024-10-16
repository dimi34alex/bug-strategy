using UnityEngine;
using Zenject;
using BugStrategy.Constructions;
using BugStrategy.Missions;

// ������ ��� ����������� ��������� ������, ���� ��������, �� ���� ������������, ����� ��������
namespace BugStrategy.Constructions
{
    public class ConstructionSelectionLogger : MonoBehaviour
    {
        [Inject] private MissionData _missionData; // ���������� � ������ ������
        private ConstructionBase _previousSelectedConstruction; // ���������� ��������� ������ ��� ���������

        private void Start()
        {
            // ���������, ��� ������ ������ ������ ��������
            if (_missionData.ConstructionSelector == null)
            {
                Debug.LogError("ConstructionSelector not found in MissionData.");
            }
            else
            {
                // ������������� �� ������� ��������� ������
                _missionData.ConstructionSelector.OnSelectionChange += LogSelectionChange;
            }
        }

        private void OnDestroy()
        {
            // ������������ �� ������� ��� ����������� �������
            if (_missionData.ConstructionSelector != null)
            {
                _missionData.ConstructionSelector.OnSelectionChange -= LogSelectionChange;
            }
        }

        // �������� ����� ������ ������
        private void LogSelectionChange()
        {
            ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

            if (selectedConstruction != _previousSelectedConstruction)
            {
                if (selectedConstruction == null)
                {
                    Debug.Log("No construction selected."); // ���, ���� ������ �� �������
                }
                else
                {
                    LogConstructionSelection(selectedConstruction); // �������� ��������� ������
                }

                _previousSelectedConstruction = selectedConstruction; // ��������� ���������� ��������� ������
            }
        }

        // ������� ���������� � ��������� ������ � ���
        private void LogConstructionSelection(ConstructionBase construction)
        {
            Debug.Log($"Player selected a construction of type {construction.ConstructionID} " +
                      $"(Affiliation: {construction.Affiliation}).");
        }
    }
}