using BugStrategy.Constructions;

namespace BugStrategy.Tiles.WarFog
{
    public class ConstructionVisibleWarFogZone : VisibleWarFogZone
    {
        private ConstructionBase _constructionBase;
        
        private void Start () 
            => _constructionBase = transform.GetComponentInParent<ConstructionBase>();

        protected override AffiliationEnum GetAffiliation() 
            => _constructionBase.Affiliation;
    }
}
