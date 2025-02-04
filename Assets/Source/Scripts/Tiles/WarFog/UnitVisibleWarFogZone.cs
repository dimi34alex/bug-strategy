using BugStrategy.Unit;

namespace BugStrategy.Tiles.WarFog
{
    public class UnitVisibleWarFogZone : VisibleWarFogZone
    {
        private UnitBase _unitBase;

        private void Start () 
            => _unitBase = transform.GetComponentInParent<UnitBase>();

        protected override AffiliationEnum GetAffiliation() 
            => _unitBase.Affiliation;
    }
}
