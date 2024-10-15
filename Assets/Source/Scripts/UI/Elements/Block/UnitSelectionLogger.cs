using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

// ������ ��� ����������� ��������� ������, ���� ��������, �� ���� ������������, ����� ��������
namespace BugStrategy.Unit.UnitSelection
{
    public class UnitSelectionLogger : MonoBehaviour
    {
        private UnitSelection _unitSelection; // ��������� ��� ������ ������
        private List<UnitBase> _previousSelectedUnits = new List<UnitBase>(); // �������� ���������� ��������� ������

        private void Awake()
        {
            // ����� ���������� ������ ������
            _unitSelection = FindObjectOfType<UnitSelection>();

            if (_unitSelection == null)
            {
                Debug.LogError("UnitSelection component not found."); // ���, ���� ��������� �� ������
            }
        }

        private void Update()
        {
            if (_unitSelection != null)
            {
                // �������� ������� ��������� �����
                List<UnitBase> selectedUnits = _unitSelection.GetSelectedUnits();

                // ���������, ��������� �� ����� ������
                if (!AreListsEqual(selectedUnits, _previousSelectedUnits))
                {
                    LogSelectionChange(selectedUnits); // �������� ����� ������
                    _previousSelectedUnits = new List<UnitBase>(selectedUnits); // ��������� ������ ��������� ������
                }
            }
        }

        // ���������, ��������� �� ��� ������ ������
        private bool AreListsEqual(List<UnitBase> listA, List<UnitBase> listB)
        {
            if (listA.Count != listB.Count)
                return false;

            for (int i = 0; i < listA.Count; i++)
            {
                if (listA[i] != listB[i])
                    return false;
            }
            return true;
        }

        // �������� ���������� � ����� ������ ������
        private void LogSelectionChange(List<UnitBase> selectedUnits)
        {
            if (selectedUnits.Count == 0)
            {
                Debug.Log("No units selected."); // ���, ���� ����� �� �������
                return;
            }

            // ������������ ���������� ��������� ������ �� ����
            Dictionary<string, int> unitTypeCounts = new Dictionary<string, int>();

            foreach (UnitBase unit in selectedUnits)
            {
                string unitTypeName = unit.UnitType.ToString();

                if (unitTypeCounts.ContainsKey(unitTypeName))
                {
                    unitTypeCounts[unitTypeName]++;
                }
                else
                {
                    unitTypeCounts[unitTypeName] = 1;
                }
            }

            LogUnitSelection(unitTypeCounts); // �������� ������������ �����
        }

        // ������� � ��� ���������� � ��������� ������
        private void LogUnitSelection(Dictionary<string, int> unitTypeCounts)
        {
            if (unitTypeCounts.Count == 1)
            {
                foreach (var unitType in unitTypeCounts)
                {
                    Debug.Log($"Player selected {unitType.Value} unit(s) of type {unitType.Key}."); // ��� ������ ���� �����
                }
            }
            else
            {
                Debug.Log("Player selected the following units:"); // ���, ���� ������� ��������� ����� ������

                foreach (var unitType in unitTypeCounts)
                {
                    Debug.Log($"{unitType.Value} unit(s) of type {unitType.Key}."); // ��� ������� ���� ������
                }
            }
        }
    }
}