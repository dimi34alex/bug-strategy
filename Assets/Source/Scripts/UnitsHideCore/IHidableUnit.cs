namespace UnitsHideCore
{
    public interface IHidableUnit
    {
        public HiderCellBase TakeHideCell();
        
        public void LoadHideCell(HiderCellBase hiderCell);
    }
}