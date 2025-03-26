using BugStrategy.Missions;
using UnityEngine;
using Zenject;

namespace BugStrategy.MiniMap
{
    public class MiniMapObjViewFactory : IMiniMapObjViewFactory
    {
        private readonly DiContainer _diContainer;
        private readonly MiniMapObjViewConfig _config;
        private readonly MissionData _missionData;

        private MiniMapObjView Prefab => _config.MiniMapObjViewPrefab;
        
        protected MiniMapObjViewFactory(DiContainer diContainer, MiniMapObjViewConfig config, MissionData missionData)
        {
            _diContainer = diContainer;
            _config = config;
            _missionData = missionData;
        }

        public void CreateUnitIcon(Transform parent, AffiliationEnum ownerAffiliation)
        { 
            var miniMapView = _diContainer.InstantiatePrefab(Prefab, parent).GetComponent<MiniMapObjView>();
            miniMapView.Initialize(_config.UnitIcon, GetColor(ownerAffiliation));
        }
        
        public void CreateConstructionIcon(Transform parent, AffiliationEnum ownerAffiliation)
        { 
            var miniMapView = _diContainer.InstantiatePrefab(Prefab, parent).GetComponent<MiniMapObjView>();
            miniMapView.Initialize(_config.ConstructionIcon, GetColor(ownerAffiliation));
        }
        
        public void CreateResourceSourceIcon(Transform parent, AffiliationEnum ownerAffiliation)
        { 
            var miniMapView = _diContainer.InstantiatePrefab(Prefab, parent).GetComponent<MiniMapObjView>();
            miniMapView.Initialize(_config.ResourceSourceIcon, GetColor(ownerAffiliation));
        }

        private Color GetColor(AffiliationEnum ownerAffiliation)
        {
            if (ownerAffiliation is AffiliationEnum.None or AffiliationEnum.Neutral)
                return _config.NeutralColor;

            if (ownerAffiliation == _missionData.PlayerAffiliation)
                return _config.FriendlyColor;

            return _config.EnemyColor;
        }
    }
}