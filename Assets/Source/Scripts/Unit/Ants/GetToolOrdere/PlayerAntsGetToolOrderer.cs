using BugStrategy.Constructions;
using BugStrategy.Unit.UnitSelection;

namespace BugStrategy.Unit.Ants.GetToolOrdere
{
    public class PlayerAntsGetToolOrderer
    {
        private readonly PlayerUnitsSelector _playerUnitsSelector;

        public PlayerAntsGetToolOrderer(PlayerUnitsSelector playerUnitsSelector)
        {
            _playerUnitsSelector = playerUnitsSelector;
        }

        public void GiveOrder(AntWorkshopBase workshop, UnitType unitType, ProfessionType targetProfession, int targetRang)
        {
            var allSelectedSpecificUnits = _playerUnitsSelector.GetSelectedUnits(unitType);

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