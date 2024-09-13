using System.Collections.Generic;
using Construction.TownHalls;
using CustomTimer;
using UnityEngine;

namespace Source.Scripts.Ai.MainAis
{
    public class CombatAi
    {
        private readonly AffiliationEnum _affiliation;
        private readonly UnitsAiRepository _unitsAiRepository;
        private readonly Timer _timerBeforeAttackPlayerTownHall;
        private readonly List<UnitType> _combatUnits = new List<UnitType>()
        {
            UnitType.Bumblebee,
            UnitType.Wasp,
            UnitType.Sawyer,
            UnitType.Hornet,
            UnitType.Horntail,
            UnitType.Murmur,
            UnitType.Truten,
            UnitType.HoneyCatapult,
            UnitType.MobileHive,
        };
        
        public CombatAi(AffiliationEnum affiliation, float timeBeforeAttackPlayerTownHall, UnitsAiRepository unitsAiRepository)
        {
            _affiliation = affiliation;
            _unitsAiRepository = unitsAiRepository;
            _timerBeforeAttackPlayerTownHall = new Timer(timeBeforeAttackPlayerTownHall);
            _timerBeforeAttackPlayerTownHall.OnTimerEnd += TryAttackPlayerTownHall;

            _unitsAiRepository.UnitAdded += SetRandomPos;
        }
        
        public void HandleUpdate(float deltaTime)
        {
            _timerBeforeAttackPlayerTownHall.Tick(deltaTime);
        }

        private void SetRandomPos(UnitBase unit)
        {
            if (_combatUnits.Contains(unit.UnitType))
            {
                var movePos = GetRandomPositionXZ(unit.transform.position, 4, 12);
                unit.HandleGiveOrder(null, UnitPathType.Move, movePos);
            }
        }

        private Vector3 GetRandomPositionXZ(Vector3 center, float minDistance, float maxDistance)
        {
            var x = Random.value;
            var z = Random.value;

            var dir = new Vector3(x, 0, z).normalized;
            var distance = Random.Range(0, maxDistance);

            return center + dir * distance;
        }
        
        private void TryAttackPlayerTownHall()
        {
            _timerBeforeAttackPlayerTownHall.SetMaxValue(5);

            var townHalls = Object.FindObjectsOfType<TownHallBase>();//TODO: change this shit
            TownHallBase enemyTownHall = null;
            foreach (var townHall in townHalls)
            {
                if (townHall.Affiliation != _affiliation)
                {
                    enemyTownHall = townHall;
                    break;
                }
            }

            if (enemyTownHall == null)
                return;

            foreach (var combatUnitType in _combatUnits)
            {
                if (!_unitsAiRepository.Ais.ContainsKey(combatUnitType)) 
                    continue;
                
                foreach (var ai in _unitsAiRepository.Ais[combatUnitType])
                {
                    if (ai.AiState == AiUnitStateType.Idle)
                        ai.SetOrderPriority(enemyTownHall, AiUnitStateType.Attack);
                }   
            }
        }
    }
}