using System;
using Unit.ProfessionsCore.Processors;
using UnityEngine;

namespace Unit.ProfessionsCore
{
    [Serializable]
    public abstract class ProfessionBase : IReadOnlyProfession
    {
        protected readonly UnitBase Unit;
        protected readonly ProfessionInteractionZoneProcessor ProfessionInteractionZoneProcessor;
        
        public abstract ProfessionType ProfessionType { get; } 
        public event Action OnEnterInZone;
        
        protected AffiliationEnum Affiliation => Unit.Affiliation;
        protected Transform Transform => Unit.transform;
        
        protected ProfessionBase(UnitBase unit, float interactionRange)
        {
            ProfessionInteractionZoneProcessor = new ProfessionInteractionZoneProcessor(unit.ProfessionInteractionZone, interactionRange);
            ProfessionInteractionZoneProcessor.OnEnterInZone += EnterInZone;
            Unit = unit;
        }

        protected void EnterInZone() => OnEnterInZone?.Invoke();
        
        public virtual void HandleUpdate(float time) { }
        
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

        public abstract UnitPathData AutoGiveOrder(IUnitTarget unitTarget);
        
        public UnitPathData HandleGiveOrder(IUnitTarget unitTarget, UnitPathType pathType) 
            => new UnitPathData(unitTarget, ValidateHandleOrder(unitTarget, pathType));
        
        protected abstract UnitPathType ValidateHandleOrder(IUnitTarget target, UnitPathType pathType);
    } 
}
