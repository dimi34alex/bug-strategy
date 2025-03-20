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

        [Inject] private UIAbilitiesConfig _uiAbilitiesConfig;
        
        protected override void Initialize()
        {
            base.Initialize();

            healthBar.Init(selectable.HealthStorage);
            selectionField.Init(selectable.IsSelected);

            if (selectable.TryCast(out IAbilitiesOwner abilitiesOwner)) 
                abilitiesBar.Init(abilitiesOwner.Abilities, _uiAbilitiesConfig);
        }

        protected override void OnSelect(bool isFullView)
        {
            base.OnSelect(isFullView);

            healthBar.OnSelect();

            if (isFullView)
            {
                abilitiesBar.OnSelect();
                selectionField.OnSelect();
            }
        }

        protected override  void OnDeselect()
        {
            base.OnDeselect();
            
            healthBar.OnDeselect();
            selectionField.OnDeselect();
            abilitiesBar.OnDeselect();
        }
    }
}