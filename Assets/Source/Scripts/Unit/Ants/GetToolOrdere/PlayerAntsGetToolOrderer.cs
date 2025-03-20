using BugStrategy.Constructions;
using BugStrategy.Unit.UnitSelection;

namespace BugStrategy.Unit.Ants.GetToolOrdere
{
    public class PlayerAntsGetToolOrderer
    {
        private readonly UnitsSelector _unitsSelector;

        public PlayerAntsGetToolOrderer(UnitsSelector unitsSelector)
        {
            _unitsSelector = unitsSelector;
        }

        public void GiveOrder(AntWorkshopBase workshop, UnitType unitType, ProfessionType targetProfession, int targetRang)
        {
            var allSelectedSpecificUnits = _unitsSelector.GetPlayerSelectedUnits(unitType);

            foreach (var unitBase  in allSelectedSpecificUnits)
            {
                var ant = unitBase as AntBase;
                if (ant.CurrentProfession.ProfessionType != targetProfession || 
                    ant.CurrentProfession.ProfessionRang < targetRang)
                {
                    ant.GiveOrderSwitchProfession(workshop, targetProfession, targetRang);
                }
            }
        }
    }
}