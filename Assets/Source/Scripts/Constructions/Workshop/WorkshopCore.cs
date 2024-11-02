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
        }
        
        public void SetCapacity(int newCapacity)
        {
            CapacityPerTool = newCapacity;
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
                return false;
            }
            
            _professionTools[unitType][rang]--;
            return true;
        }
        
        private void InitDictionary(UnitType unitType)
        {
            var rangsCount = _antProfessionsConfigsRepository.GetRangsCount(unitType, _professionType);
            _professionTools.Add(unitType, new Dictionary<int, int>(rangsCount));
            for (int i = 0; i < rangsCount; i++) 
                _professionTools[unitType].Add(i, 0);
        }
    }
}