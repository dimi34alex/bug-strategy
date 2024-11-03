using BugStrategy.Constructions;
using BugStrategy.Unit.UnitSelection;

namespace BugStrategy.Unit.Ants.GetToolOrdere
{
    public class PlayerAntsGetToolOrderer
    {
        private readonly UnitSelector _unitSelector;

        public PlayerAntsGetToolOrderer(UnitSelector unitSelector)
        {
            _unitSelector = unitSelector;
        }

        public void GiveOrder(AntWorkshopBase workshop, UnitType unitType, ProfessionType targetProfession, int targetRang)
        {
            var allSelectedSpecificUnits = _unitSelector.GetSelectedUnits(unitType);

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