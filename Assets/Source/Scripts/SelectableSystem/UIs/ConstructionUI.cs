using BugStrategy.Constructions;
using UnityEngine;

namespace BugStrategy.SelectableSystem
{
    public class ConstructionUI : SelectableObjectUIBase<ConstructionBase>
    {
        [SerializeField] private HealthBar healthBar;
    
        protected override void Initialize()
        {
            base.Initialize();
        
            healthBar.Init(selectable.HealthStorage);
        }

        protected override void OnSelect(bool isFullView)
        {
            base.OnSelect(isFullView);

            healthBar.OnSelect();
        }

        protected override void OnDeselect()
        {
            base.OnDeselect();

            healthBar.OnDeselect();
        }
    }
}