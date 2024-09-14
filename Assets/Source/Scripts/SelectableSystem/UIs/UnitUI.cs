using BugStrategy.Unit;
using BugStrategy.Unit.AbilitiesCore;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.SelectableSystem
{
    public class UnitUI : SelectableObjectUIBase<UnitBase>
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private SelectionField selectionField;
        [SerializeField] private AbilitiesBar abilitiesBar;

        [Inject] private AbilitiesUiConfig _abilitiesUiConfig;
        
        protected override void OnStart()
        {
            base.OnStart();

            OnSelectionUI[] onSelectionUIs = { healthBar, selectionField, abilitiesBar };

            healthBar.Init(selectable.HealthStorage);
            selectionField.Init(selectable.IsSelected);

            if (selectable.TryCast(out IAbilitiesOwner abilitiesOwner)) 
                abilitiesBar.Init(abilitiesOwner.Abilities, _abilitiesUiConfig);

            foreach (var OoSelectionUI in onSelectionUIs)
            {
                SelectedEvent += OoSelectionUI.OnSelect;
                DeselectedEvent += OoSelectionUI.OnDeselect;
            } 
        }
    }
}