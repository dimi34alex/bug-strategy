using System;
using Unit.ProcessorsCore;
using UnityEngine;

namespace Unit.OrderValidatorCore
{
    [Serializable]
    public abstract class OrderValidatorBase
    {
        protected readonly UnitBase Unit;
        protected readonly ProfessionInteractionZoneProcessor ProfessionInteractionZoneProcessor;
        
        public event Action OnEnterInZone;
        
        protected AffiliationEnum Affiliation => Unit.Affiliation;
        protected Transform Transform => Unit.transform;
        
        protected OrderValidatorBase(UnitBase unit, float interactionRange)
        {
            ProfessionInteractionZoneProcessor = new ProfessionInteractionZoneProcessor(unit.UnitInteractionZone, interactionRange);
            ProfessionInteractionZoneProcessor.OnEnterInZone += EnterInZone;
            Unit = unit;
        }

        protected void EnterInZone()
            => OnEnterInZone?.Invoke();

        /// <returns>
        /// return distance between unit and someTarget
        /// </returns>
        protected float Distance(IUnitTarget someTarget) => Vector3.Distance(Transform.position, someTarget.Transform.position);
    
        /// <returns>
        /// return true if someTarget stay in interaction zone, else return false
        /// </returns>
        protected bool CheckInteraction(IUnitTarget someTarget) => ProfessionInteractionZoneProcessor.Contains(someTarget);

        /// <returns>
        /// return true if unit can doing something with target in pathData on current distance, else return false
        /// </returns>
        public virtual bool CheckDistance(UnitPathData pathData) => CheckInteraction(pathData.Target);

        public UnitPathData AutoGiveOrder(IUnitTarget target)
        {
            if (!target.IsAnyNull() && !target.IsActive)
                target = null;
            return ValidateAutoOrder(target);
        }
        
        protected abstract UnitPathData ValidateAutoOrder(IUnitTarget target);
        
        public UnitPathData HandleGiveOrder(IUnitTarget target, UnitPathType pathType)
        {
            if (!target.IsAnyNull() && !target.IsActive)
                target = null;
            return new UnitPathData(target, ValidateHandleOrder(target, pathType));
        }

        protected abstract UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType);
    } 
}
