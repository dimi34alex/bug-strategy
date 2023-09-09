using UnityEngine;

namespace SelectableSystem
{
    public class ConstructionUI : SelectableObjectUIBase<ConstructionBase>
    {
        [SerializeField] private HealthBar healthBar;
    
        protected override void OnStart()
        {
            base.OnStart();
        
            healthBar.Init(selectable.HealthStorage);
            SelectedEvent += healthBar.OnSelect;
            DeselectedEvent += healthBar.OnDeselect;
        }
    }
}