using UnityEngine;

namespace SelectableSystem
{
    public class UnitUI : SelectableObjectUIBase<UnitBase>
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private SelectionField selectionField;
        [SerializeField] private AbilitiesBar abilitiesBar;

        protected override void OnStart()
        {
            base.OnStart();
            
            healthBar.Init(selectable.HealthStorage);
            SelectedEvent += healthBar.OnSelect;
            DeselectedEvent += healthBar.OnDeselect;

            abilitiesBar.Init(selectable.Abilities);
            SelectedEvent += abilitiesBar.OnSelect;
            DeselectedEvent += abilitiesBar.OnDeselect;

            selectionField.Init(selectable.IsSelected);
            SelectedEvent += selectionField.OnSelect;
            DeselectedEvent += selectionField.OnDeselect;
        }
    }
}