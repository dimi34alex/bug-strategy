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

            OnSelectionUI[] onSelectionUIs = { healthBar, selectionField, abilitiesBar };

            healthBar.Init(selectable.HealthStorage);
            abilitiesBar.Init(selectable.Abilities);
            selectionField.Init(selectable.IsSelected);

            foreach (var OoSelectionUI in onSelectionUIs)
            {
                SelectedEvent += OoSelectionUI.OnSelect;
                DeselectedEvent += OoSelectionUI.OnDeselect;
            } 
        }
    }
}