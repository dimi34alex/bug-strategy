using BugStrategy.UnitsHideCore;

namespace BugStrategy.Unit.Ants
{
    public class AntHiderCell : HiderCellBase
    {
        public readonly ProfessionType ProfessionType;
        public readonly int ProfessionRang;

        public AntHiderCell(AntBase ant) 
            : base(ant)
        {
            ProfessionType = ant.CurProfessionType;
            ProfessionRang = ant.CurProfessionRang;
        }
    }
}