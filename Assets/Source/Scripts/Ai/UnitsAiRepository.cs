using System;
using System.Collections.Generic;
using Source.Scripts.Ai.InternalAis;
using Unit.Factory;

namespace Source.Scripts.Ai
{
    public class UnitsAiRepository
    {
        private readonly AffiliationEnum _affiliation;
        private readonly UnitFactory _unitFactory;
        private readonly Dictionary<UnitType, List<InternalAiBase>> _ais = 
            new Dictionary<UnitType, List<InternalAiBase>>();

        public IReadOnlyDictionary<UnitType, List<InternalAiBase>> Ais => _ais;

        public event Action<UnitBase> UnitAdded; 
        
        public UnitsAiRepository(AffiliationEnum affiliation, UnitFactory unitFactory)
        {
            _affiliation = affiliation;
            _unitFactory = unitFactory;

            _unitFactory.UnitCreated += TryAdd;
        }

        public int UnitCount(UnitType unitType)
            => !_ais.ContainsKey(unitType) ? 0 : _ais[unitType].Count;

        private void TryAdd(UnitBase unit)
        {
            if(unit.Affiliation != _affiliation)
                return;
            
            if(!_ais.ContainsKey(unit.UnitType))
                _ais.Add(unit.UnitType, new List<InternalAiBase>());
            
            unit.OnUnitDied += RemoveAi;
            _ais[unit.UnitType].Add(unit.InternalAi);
            unit.InternalAi.SetOrderPriority(null, AiUnitStateType.Idle);
            
            UnitAdded?.Invoke(unit);
        }

        private void RemoveAi(UnitBase unit)
        {
            if (_ais.TryGetValue(unit.UnitType, out var ais))
            {
                unit.OnUnitDied -= RemoveAi;
                ais.Remove(unit.InternalAi);
            }
        }
    }
}