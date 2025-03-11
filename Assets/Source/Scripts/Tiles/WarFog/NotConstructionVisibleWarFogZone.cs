using BugStrategy.NotConstructions;

namespace BugStrategy.Tiles.WarFog
{
    public class NotConstructionVisibleWarFogZone : VisibleWarFogZone
    {
        private NotConstructionBase _notConstructionBase;
        
        private void Start () 
            => _notConstructionBase = transform.GetComponentInParent<NotConstructionBase>();

        protected override AffiliationEnum GetAffiliation() 
            => _notConstructionBase.Affiliation;
    }
}
