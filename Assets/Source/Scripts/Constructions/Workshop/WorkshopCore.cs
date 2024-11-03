using System;
using System.Collections.Generic;
using BugStrategy.Unit;
using BugStrategy.Unit.Ants;

namespace BugStrategy.Constructions
{
    public class WorkshopCore
    {
        private readonly ProfessionType _professionType;
        private readonly AntProfessionsConfigsRepository _antProfessionsConfigsRepository;

        public int CapacityPerTool { get; private set; }
        public int RangAccess { get; private set; }

        public Action OnChange;

        /// <summary>
        /// value - (tool rang, count of tools)
        /// </summary>
        private readonly Dictionary<UnitType, Dictionary<int, int>> _professionTools = new();
        
        public WorkshopCore(ProfessionType professionType, AntProfessionsConfigsRepository antProfessionsConfigsRepository)
        {
            _professionType = professionType;
            _antProfessionsConfigsRepository = antProfessionsConfigsRepository;

            InitDictionary(UnitType.AntStandard);
            InitDictionary(UnitType.AntBig);
            InitDictionary(UnitType.AntFlying);
        }

        public void SetRangAccess(int newRangAccess)
        {
            RangAccess = newRangAccess;
            OnChange?.Invoke();
        }
        
        public void SetCapacity(int newCapacity)
        {
            CapacityPerTool = newCapacity;
            OnChange?.Invoke();
        }

        public void CreateTool(UnitType unitType, int rang)
        {
            if (RangAccess < rang ||
                !_professionTools.ContainsKey(unitType) ||
                !_professionTools[unitType].ContainsKey(rang) ||
                _professionTools[unitType][rang] >= CapacityPerTool)
            {
                return;
            }

            _professionTools[unitType][rang]++;
            OnChange?.Invoke();
        }
        
        /// <summary>
        /// on success decrease tool count
        /// </summary>
        public bool GetTool(UnitType unitType, int rang, out AntProfessionConfigBase config)
        {
            if (!_antProfessionsConfigsRepository.TryGetAntConfig(unitType, _professionType, rang, out config) ||
                RangAccess < rang ||
                !_professionTools.ContainsKey(unitType) ||
                _professionTools[unitType].ContainsKey(rang) ||
                _professionTools[unitType][rang] <= 0)
            {
                OnChange?.Invoke();
                return false;
            }
            
            _professionTools[unitType][rang]--;
            OnChange?.Invoke();
            return true;
        }

        public IReadOnlyDictionary<int, int> GetToolData(UnitType unitType) 
            => _professionTools[unitType];

        private void InitDictionary(UnitType unitType)
        {
            var rangsCount = _antProfessionsConfigsRepository.GetRangsCount(unitType, _professionType);
            _professionTools.Add(unitType, new Dictionary<int, int>(rangsCount));
            for (int i = 0; i < rangsCount; i++) 
                _professionTools[unitType].Add(i, 0);
        }
    }
}