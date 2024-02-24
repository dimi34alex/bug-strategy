using UnityEngine;

namespace Unit.Ants.States
{
    public class AntSwitchProfessionState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.SwitchProfession;

        private readonly AntBase _ant;
        
        public AntSwitchProfessionState(AntBase ant)
        {
            _ant = ant;
        }

        public override void OnStateEnter()
        {
            if (_ant.CurrentPathData.TargetType != UnitTargetType.Construction ||
                _ant.CurrentPathData.Target.IsAnyNull() ||
                !_ant.CurrentPathData.Target.TryCast(out SwitchAntProfessionCunstruction professionConstruction))
            {
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurrentPathData.TargetType} | " +
                                 $"{_ant.CurrentPathData.Target.IsAnyNull()} | " +
                                 $"{!_ant.CurrentPathData.Target.TryCast(out professionConstruction)}");
                
                _ant.AutoGiveOrder(null);
                return;
            }
            
            if (!professionConstruction.TryTakeConfig(_ant.TargetProfessionType, _ant.TargetProfessionRang, out var config))
            {
                _ant.AutoGiveOrder(_ant.CurrentPathData.Target);
                Debug.LogWarning($"Config is null: {_ant.TargetProfessionType}, {_ant.TargetProfessionRang}");
            }
            else
                _ant.SwitchProfession(config);
        }

        public override void OnStateExit()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
    }
}