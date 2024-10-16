using UnityEngine;
using Zenject;
using BugStrategy.Constructions;
using BugStrategy.Missions;

// Скрипт для логирования выделения зданий, пока отключен, но если понадобиться, можно включить
namespace BugStrategy.Constructions
{
    public class ConstructionSelectionLogger : MonoBehaviour
    {
        [Inject] private MissionData _missionData; // информация о выборе зданий
        private ConstructionBase _previousSelectedConstruction; // Предыдущее выбранное здание для сравнения

        private void Start()
        {
            // Проверяем, что объект выбора зданий доступен
            if (_missionData.ConstructionSelector == null)
            {
                Debug.LogError("ConstructionSelector not found in MissionData.");
            }
            else
            {
                // Подписываемся на событие изменения выбора
                _missionData.ConstructionSelector.OnSelectionChange += LogSelectionChange;
            }
        }

        private void OnDestroy()
        {
            // Отписываемся от события при уничтожении объекта
            if (_missionData.ConstructionSelector != null)
            {
                _missionData.ConstructionSelector.OnSelectionChange -= LogSelectionChange;
            }
        }

        // Логируем смену выбора здания
        private void LogSelectionChange()
        {
            ConstructionBase selectedConstruction = _missionData.ConstructionSelector.SelectedConstruction;

            if (selectedConstruction != _previousSelectedConstruction)
            {
                if (selectedConstruction == null)
                {
                    Debug.Log("No construction selected."); // Лог, если здание не выбрано
                }
                else
                {
                    LogConstructionSelection(selectedConstruction); // Логируем выбранное здание
                }

                _previousSelectedConstruction = selectedConstruction; // Обновляем предыдущее выбранное здание
            }
        }

        // Выводим информацию о выбранном здании в лог
        private void LogConstructionSelection(ConstructionBase construction)
        {
            Debug.Log($"Player selected a construction of type {construction.ConstructionID} " +
                      $"(Affiliation: {construction.Affiliation}).");
        }
    }
}