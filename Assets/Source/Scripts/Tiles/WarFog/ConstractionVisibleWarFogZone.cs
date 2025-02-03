using BugStrategy.Constructions;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog
{
    public class ConstractionVisibleWarFogZone : VisibleWarFogZone
    {
        [SerializeField] private ConstructionBase ScriptWithConstructionBase;
        
        private void Start ()
        {
            ScriptWithConstructionBase = transform.GetComponentInParent<ConstructionBase>();
        }

        protected override AffiliationEnum GetAffiliation() 
            => ScriptWithConstructionBase.Affiliation;
    }
}
