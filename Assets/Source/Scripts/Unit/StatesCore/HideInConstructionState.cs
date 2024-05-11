using System;
using UnitsHideCore;
using UnityEngine;

namespace Unit.States
{
    public class HideInConstructionState : EntityStateBase
    {
        public override EntityStateID EntityStateID => EntityStateID.HideInConstruction;

        private readonly UnitBase _unitBase;
        private readonly IHidableUnit _hidableUnit;
        private readonly Action _onHideUnitMethod;
        
        public override event Action StateExecuted;
        
        public HideInConstructionState(UnitBase unitBase, IHidableUnit hidableUnit, Action onHideUnitMethod)
        {
            _unitBase = unitBase;
            _hidableUnit = hidableUnit;
            _onHideUnitMethod = onHideUnitMethod;
        }
        
        public override void OnStateEnter()
        {
            if (_unitBase.CurrentPathData.Target.TryCast(out IHiderConstruction hiderConstruction))
            {
                if(hiderConstruction.Hider.TryHideUnit(_hidableUnit))
                    _onHideUnitMethod?.Invoke();
                else
                    // _unitBase.AutoGiveOrder(null);
                    StateExecuted?.Invoke();
            }
            else
            {
                Debug.LogWarning("target cant be casted to hider construction");
                // _unitBase.AutoGiveOrder(null);
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