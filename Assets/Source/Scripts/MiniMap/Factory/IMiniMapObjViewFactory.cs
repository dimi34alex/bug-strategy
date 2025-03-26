using UnityEngine;

namespace BugStrategy.MiniMap
{
    public interface IMiniMapObjViewFactory
    {
        public void CreateUnitIcon(Transform parent, AffiliationEnum ownerAffiliation);
        public void CreateConstructionIcon(Transform parent, AffiliationEnum ownerAffiliation);
        public void CreateResourceSourceIcon(Transform parent, AffiliationEnum ownerAffiliation);
    }
}