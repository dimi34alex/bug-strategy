using System;
using BugStrategy.Constructions;
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
                !_ant.CurrentPathData.Target.TryCast(out AntWorkshopBase antWorkshopBase))
            {
                Debug.LogWarning($"Some problem: " +
                                 $"{_ant.CurrentPathData.TargetType} | " +
                                 $"{_ant.CurrentPathData.Target.IsAnyNull()} | " +
                                 $"{!_ant.CurrentPathData.Target.TryCast(out antWorkshopBase)}");
                
                // _ant.AutoGiveOrder(null);
                StateExecuted?.Invoke();
                return;
            }
            
            if (!antWorkshopBase.WorkshopCore.TryGetTool(_ant.UnitType, _ant.TargetProfessionRang, out var config))
            {
                Debug.LogWarning($"Cant get tool: {_ant.TargetProfessionType}, {_ant.TargetProfessionRang}");
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