using System;
using Unit.Ants.Configs.Professions;
using Unit.OrderValidatorCore;

namespace Unit.Ants.Professions
{
    public abstract class AntProfessionBase
    {
        private readonly AntProfessionRang _antProfessionRang;
        public int ProfessionRang => _antProfessionRang.Rang;
        
        public abstract ProfessionType ProfessionType { get; }
        public abstract OrderValidatorBase OrderValidatorBase { get; }
        
        public event Action OnEnterInZone;

        public AntProfessionBase(AntProfessionRang antProfessionRang)
        {
            _antProfessionRang = antProfessionRang;
        }
        
        public virtual void HandleUpdate(float time)
        {
            
        }

        protected void EnterInZone()
            => OnEnterInZone?.Invoke();
    }
}