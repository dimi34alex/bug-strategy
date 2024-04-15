using UnitsHideCore;

namespace Constructions.BeeHospital
{
    public class HealProcessor
    {
        private readonly IHider _hider;
        
        public float HealPerSecond { get; private set; }
        
        public HealProcessor(IHider hider, float startHealPerSecond)
        {
            _hider = hider;
            HealPerSecond = startHealPerSecond;
        }

        public void HandleUpdate(float time)
        {
            foreach (var hiderCell in _hider.HiderCells)
                hiderCell.Heal(HealPerSecond * time);
        }

        public void SetHealPerSecond(float newHealPerSecond)
        {
            HealPerSecond = newHealPerSecond;
        }
    }
}