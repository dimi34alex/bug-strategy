using Construction.TownHalls;
using Unit.ProcessorsCore;
using Object = UnityEngine.Object;

namespace Source.Scripts.Ai.InternalAis
{
    public class StorageResourceStateOrderEvaluator : UnitInternalEvaluator
    {
        private readonly IReadOnlyResourceExtractionProcessor _resourceExtractionProcessor;
        private TownHallBase _hashedTownHall;
        
        public StorageResourceStateOrderEvaluator(UnitBase unit, InternalAiBase internalAi
            , IReadOnlyResourceExtractionProcessor resourceExtractionProcessor) 
            : base(unit, internalAi)
        {
            _resourceExtractionProcessor = resourceExtractionProcessor;
        }

        public override float Evaluate()
        {
            switch (UnitInternalAi.AiState)
            {
                case AiUnitStateType.Auto:
                    if (_resourceExtractionProcessor.GotResource)
                    {
                        if (Unit.CurrentPathData.Target.IsAnyNull() &&
                            Unit.CurrentPathData.Target.TryCast(out TownHallBase townHall))
                            _hashedTownHall = townHall;
                        
                        if (!_hashedTownHall.IsAnyNull() && _hashedTownHall.Affiliation == Unit.Affiliation)
                            return 1;
                        
                        var townHalls = Object.FindObjectsOfType<TownHallBase>();//TODO: change this shit
                        foreach (var someTownHall in townHalls)
                            if (someTownHall.Affiliation == Unit.Affiliation)
                            {
                                _hashedTownHall = someTownHall;
                                return 1;
                            }
                    }
                    break;
                case AiUnitStateType.CollectResource:
                    if (_resourceExtractionProcessor.GotResource)
                    {
                        if (!_hashedTownHall.IsAnyNull() && _hashedTownHall.Affiliation == Unit.Affiliation)
                            return 1;
                        
                        var townHalls = Object.FindObjectsOfType<TownHallBase>();//TODO: change this shit
                        foreach (var townHall in townHalls)
                            if (townHall.Affiliation == Unit.Affiliation)
                            {
                                _hashedTownHall = townHall;
                                return 1;
                            }
                    }
                    break;
            }

            return float.MinValue;
        }

        public override void Apply()
        {
            Unit.HandleGiveOrder(_hashedTownHall, UnitPathType.Storage_Resource);
        }
    }
}