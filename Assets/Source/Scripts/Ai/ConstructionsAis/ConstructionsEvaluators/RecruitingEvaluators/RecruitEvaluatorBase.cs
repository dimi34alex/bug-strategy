using System.Collections.Generic;
using System.Linq;
using BugStrategy.Constructions.UnitsRecruitingSystem;
using BugStrategy.ResourcesSystem;
using BugStrategy.ResourcesSystem.ResourcesGlobalStorage;
using BugStrategy.Unit;

namespace BugStrategy.Ai.ConstructionsAis
{
    public abstract class RecruitEvaluatorBase : ConstructionEvaluatorBase
    {
        private readonly RecruitingEvaluationConfig _recruitingEvaluationConfig;
        private readonly IRecruitingConstruction _recruitingConstruction;
        private readonly UnitsAiRepository _unitsAiRepository;
        
        protected abstract UnitType RecruitUnitType { get; }
        
        //TODO: set it in config:
        private readonly Dictionary<ResourceID, int> _resourceThreshold;
        
        protected RecruitEvaluatorBase(UnitsAiRepository unitsAiRepository, IRecruitingConstruction recruitingConstruction, 
             RecruitingEvaluationConfig recruitingEvaluationConfig, ITeamsResourcesGlobalStorage teamsResourcesGlobalStorage) 
            : base(teamsResourcesGlobalStorage)
        {
            _unitsAiRepository = unitsAiRepository;
            _recruitingConstruction = recruitingConstruction;
            _recruitingEvaluationConfig = recruitingEvaluationConfig;
        }
        
        public override float Evaluate()
        {
            if (!_recruitingConstruction.Recruiter.HaveFreeStack())
                return float.MinValue;

            var res = _recruitingConstruction.Recruiter.UnitRecruitingData
                .FirstOrDefault(p => p.CurrentID == RecruitUnitType);
            if (res == null)
                return float.MinValue;

            var unitsCount = _unitsAiRepository.UnitCount(RecruitUnitType);
            var unitsPriorityScale = _recruitingEvaluationConfig.UnitsLimitCurve.Evaluate(unitsCount);
            if (unitsPriorityScale < 0)
                return float.MinValue;
            
            var priority = PriceCheck(res);
            priority *= unitsPriorityScale;
            if (priority <= _recruitingEvaluationConfig.PriorityThreshHold)
                priority = float.MinValue;
            
            return priority;
        }

        public override void Apply()
        {
            _recruitingConstruction.RecruitUnit(RecruitUnitType);
        }
        
        private float PriceCheck(UnitRecruitingData unitRecruitingData)
        {
            var unitCost = unitRecruitingData.Costs;
            var result = new Dictionary<ResourceID, int>();
            foreach (var cost in unitCost)
            {
                var resourcesInStorage = TeamsResourcesGlobalStorage.GetResource(_recruitingConstruction.Affiliation, cost.Key);
                result.Add(cost.Key, (int)resourcesInStorage.CurrentValue - cost.Value);
            }

            var priority = 1f;
            foreach (var res in result)
            {
                if (res.Value < 0)
                    return float.MinValue;

                if(_recruitingEvaluationConfig.ResourceLimitsCurves.ContainsKey(res.Key))
                    priority *= _recruitingEvaluationConfig.ResourceLimitsCurves[res.Key].Evaluate(res.Value);
            }
            
            return priority;
        }
    }
}