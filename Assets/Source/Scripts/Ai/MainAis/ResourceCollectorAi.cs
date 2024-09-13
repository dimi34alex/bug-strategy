using System.Collections.Generic;
using CustomTimer;

namespace Source.Scripts.Ai.MainAis
{
    public class ResourceCollectorAi
    {
        private readonly UnitsAiRepository _unitsAiRepository;
        private readonly Timer _checkTimer;
        private readonly List<UnitType> _workerUnits = new List<UnitType>()
        {
            UnitType.WorkerBee,
            UnitType.Truten
        };
        
        public ResourceCollectorAi(UnitsAiRepository unitsAiRepository)
        {
            _unitsAiRepository = unitsAiRepository;
            
            _checkTimer = new Timer(2.5f);
            _checkTimer.OnTimerEnd += CheckAis;
        }
        
        public void HandleUpdate(float deltaTime)
        {
            _checkTimer.Tick(deltaTime);
        }
        
        private void CheckAis()
        {
            _checkTimer.Reset();
            foreach (var workerUnitType in _workerUnits)
            {
                if (!_unitsAiRepository.Ais.ContainsKey(workerUnitType)) 
                    continue;
                
                foreach (var workerAi in _unitsAiRepository.Ais[workerUnitType])
                {
                    if (workerAi.AiState == AiUnitStateType.Idle)
                        workerAi.SetOrderPriority(null, AiUnitStateType.CollectResource);
                }
            }
        }
    }
}