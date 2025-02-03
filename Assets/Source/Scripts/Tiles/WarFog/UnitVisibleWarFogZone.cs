using BugStrategy.Unit;
using UnityEngine;

namespace BugStrategy.Tiles.WarFog
{
    public class UnitVisibleWarFogZone : VisibleWarFogZone
    {
        [SerializeField] private UnitBase ScriptWithUnitBase;

        private void Start ()
        {
            ScriptWithUnitBase = transform.GetComponentInParent<UnitBase>();
        }
        
        protected override AffiliationEnum GetAffiliation() 
            => ScriptWithUnitBase.Affiliation;
    }
}
