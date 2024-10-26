using System;
using BugStrategy.Unit.OrderValidatorCore;

namespace BugStrategy.Unit.Ants
{
    public abstract class AntProfessionBase
    {
        public readonly int ProfessionRang;
        
        public abstract ProfessionType ProfessionType { get; }
        public abstract OrderValidatorBase OrderValidatorBase { get; }
        
        public event Action OnEnterInZone;

        public AntProfessionBase(int antProfessionRang)
        {
            ProfessionRang = antProfessionRang;
        }
        
        public virtual void HandleUpdate(float time)
        {
            
        }

        protected void EnterInZone()
            => OnEnterInZone?.Invoke();
    }
}