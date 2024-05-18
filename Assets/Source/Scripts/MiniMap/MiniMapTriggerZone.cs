using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MiniMapSystem
{
    public class MiniMapTriggerZone : TriggerZone, IMiniMapTriggerZone
    {
        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is IMiniMapObject;
        protected override bool _refreshEnteredComponentsAfterExit => false;
        
        private readonly Dictionary<MiniMapIconID, List<IMiniMapObject>> _miniMapObjects = new Dictionary<MiniMapIconID, List<IMiniMapObject>>();
        
        public Dictionary<MiniMapIconID, IReadOnlyList<IMiniMapObject>> MiniMapObjects =>
            _miniMapObjects.ToDictionary(x => x.Key, x => x.Value as IReadOnlyList<IMiniMapObject>);
        public Vector2 Scale { get; private set; }

        public event Action<MiniMapIconID> OnObjectAdd;
        public event Action<MiniMapIconID> OnObjectRemove;
        
        void Awake()
        {
            Scale = new Vector2(transform.localScale.x, transform.localScale.z);
            
            foreach (var miniMapIconID in Enumerable.Cast<MiniMapIconID>(Enum.GetValues(typeof(MiniMapIconID))))
                _miniMapObjects.Add(miniMapIconID, new List<IMiniMapObject>());
        }

        protected override void OnEnter(ITriggerable component) => AddObject(component.Cast<IMiniMapObject>());
        protected override void OnExit(ITriggerable component) => RemoveObject(component.Cast<IMiniMapObject>());
        
        private void AddObject(IMiniMapObject miniMapObject)
        {
            MiniMapIconID iconId = GetIconId(miniMapObject);

            _miniMapObjects[iconId].Add(miniMapObject);
            OnObjectAdd?.Invoke(iconId);
        }

        private void RemoveObject(IMiniMapObject miniMapObject)
        {
            MiniMapIconID iconId = GetIconId(miniMapObject);

            if(!_miniMapObjects[iconId].Contains(miniMapObject)) 
                throw  new Exception($"Unsigned IMiniMapObject: {miniMapObject}");

            _miniMapObjects[iconId].Remove(miniMapObject);
            OnObjectRemove?.Invoke(iconId);
        }

        private MiniMapIconID GetIconId(IMiniMapObject miniMapObject)
        {
            switch (miniMapObject.Affiliation, miniMapObject.MiniMapObjectType)
            {
                case (AffiliationEnum.Team1, MiniMapObjectType.Construction):
                    return MiniMapIconID.BeeConstruction;
                case (AffiliationEnum.Team1, MiniMapObjectType.Unit):
                    return MiniMapIconID.BeeUnit;
                case (AffiliationEnum.Team2, MiniMapObjectType.Construction):
                    return MiniMapIconID.AntConstruction;
                case (AffiliationEnum.Team2, MiniMapObjectType.Unit):
                    return MiniMapIconID.AntUnit;
                case (AffiliationEnum.Team3, MiniMapObjectType.Construction):
                    return MiniMapIconID.ButterflyConstruction;
                case (AffiliationEnum.Team3, MiniMapObjectType.Unit):
                    return MiniMapIconID.ButterflyUnit;
                case (AffiliationEnum.Neutral, MiniMapObjectType.Construction):
                    return MiniMapIconID.NeutralConstruction;
                case (AffiliationEnum.Neutral, MiniMapObjectType.Unit):
                    return MiniMapIconID.NeutralUnit;
                case (AffiliationEnum.None, MiniMapObjectType.ResourceSource):
                    return MiniMapIconID.ResourceSource;
                default:
                    throw new Exception($"Unexpected pair {miniMapObject.Affiliation} | {miniMapObject.MiniMapObjectType}");
            }
        }
    }
}