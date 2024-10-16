using System.Collections.Generic;
using BugStrategy.Unit;
using UnityEngine;

// Скрипт для логирования выделения юнитов, пока отключен, но если понадобиться, можно включить
namespace BugStrategy.Unit.UnitSelection
{
    public class UnitSelectionLogger : MonoBehaviour
    {
        private UnitSelection _unitSelection; // Компонент для выбора юнитов
        private List<UnitBase> _previousSelectedUnits = new List<UnitBase>(); // Хранение предыдущих выбранных юнитов

        private void Awake()
        {
            // Поиск компонента выбора юнитов
            _unitSelection = FindObjectOfType<UnitSelection>();

            if (_unitSelection == null)
            {
                Debug.LogError("UnitSelection component not found."); // Лог, если компонент не найден
            }
        }

        private void Update()
        {
            if (_unitSelection != null)
            {
                // Получаем текущие выбранные юниты
                List<UnitBase> selectedUnits = _unitSelection.GetSelectedUnits();

                // Проверяем, изменился ли выбор юнитов
                if (!AreListsEqual(selectedUnits, _previousSelectedUnits))
                {
                    LogSelectionChange(selectedUnits); // Логируем смену выбора
                    _previousSelectedUnits = new List<UnitBase>(selectedUnits); // Обновляем список выбранных юнитов
                }
            }
        }

        // Проверяем, одинаковы ли два списка юнитов
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

        // Логируем информацию о смене выбора юнитов
        private void LogSelectionChange(List<UnitBase> selectedUnits)
        {
            if (selectedUnits.Count == 0)
            {
                Debug.Log("No units selected."); // Лог, если юниты не выбраны
                return;
            }

            // Подсчитываем количество выбранных юнитов по типу
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

            LogUnitSelection(unitTypeCounts); // Логируем подсчитанные юниты
        }

        // Выводим в лог информацию о выбранных юнитах
        private void LogUnitSelection(Dictionary<string, int> unitTypeCounts)
        {
            if (unitTypeCounts.Count == 1)
            {
                foreach (var unitType in unitTypeCounts)
                {
                    Debug.Log($"Player selected {unitType.Value} unit(s) of type {unitType.Key}."); // Лог одного типа юнита
                }
            }
            else
            {
                Debug.Log("Player selected the following units:"); // Лог, если выбрано несколько типов юнитов

                foreach (var unitType in unitTypeCounts)
                {
                    Debug.Log($"{unitType.Value} unit(s) of type {unitType.Key}."); // Лог каждого типа юнитов
                }
            }
        }
    }
}