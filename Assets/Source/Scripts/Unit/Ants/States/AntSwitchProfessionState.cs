using System;
using BugStrategy.EntityState;
using BugStrategy.Libs;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Unit.Ants
{
    public class AntSwitchProfessionState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.SwitchProfession;

        private readonly AntBase _ant;
        
        public override event Action StateExecuted;
        
        public AntSwitchProfessionState(AntBase ant)
        {
            _ant = ant;
        }

        public override void OnStateEnter()
        {
            if (_ant.CurrentPathData.TargetType != TargetType.Construction ||
                _ant.CurrentPathData.Target.IsAnyNull() ||
                !_ant.CurrentPathData.Target.TryCast(out SwitchAntProfessionCunstruction professionConstruction))
            {
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurrentPathData.TargetType} | " +
                                 $"{_ant.CurrentPathData.Target.IsAnyNull()} | " +
                                 $"{!_ant.CurrentPathData.Target.TryCast(out professionConstruction)}");
                
                // _ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            if (!professionConstruction.TryTakeConfig(_ant.UnitType, _ant.TargetProfessionType, 
                    _ant.TargetProfessionRang, out var config))
            {
                Debug.LogWarning($"Config is null: {_ant.TargetProfessionType}, {_ant.TargetProfessionRang}");
                //_ant.AutoGiveOrder(_ant.CurrentPathData.Target);
                StateExecuted?.Invoke();
            }
            else
            {
                _ant.SwitchProfession(config, _ant.TargetProfessionRang);
                //_ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
            }
        }

        public override void OnStateExit()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
    }
}