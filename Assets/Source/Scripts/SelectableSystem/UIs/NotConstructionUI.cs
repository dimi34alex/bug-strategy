using BugStrategy.NotConstructions;
using UnityEngine;

namespace BugStrategy.SelectableSystem
{
    public class NotConstructionUI : SelectableObjectUIBase<NotConstructionBase>
    {
        [SerializeField] private HealthBar healthBar;
    
        protected override void OnStart()
        {
            base.OnStart();
        
            //healthBar.Init(selectable.HealthStorage);
            //SelectedEvent += healthBar.OnSelect;
            //DeselectedEvent += healthBar.OnDeselect;
        }
    }
}