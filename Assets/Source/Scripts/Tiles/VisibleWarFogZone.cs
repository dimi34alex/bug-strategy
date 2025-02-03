using System;
using BugStrategy.Missions;
using BugStrategy.Trigger;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class VisibleWarFogZone : TriggerZone
    {
        [Inject] private readonly MissionData _missionData;

        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is Tile;
        protected override bool _refreshEnteredComponentsAfterExit => false;

        protected abstract AffiliationEnum GetAffiliation();
        
        public void SetRadius(float viewRadius)
        {
            GetComponent<SphereCollider>().radius = viewRadius;
        } 
        
        protected override void OnEnter (ITriggerable component)
        {
            if(GetAffiliation() == _missionData.PlayerAffiliation)
                component.Cast<Tile>().AddWatcher();
        } 
        
        protected override void OnExit (ITriggerable component)
        {
            if(GetAffiliation() == _missionData.PlayerAffiliation)
                component.Cast<Tile>().RemoveWatcher();
        }
    }
}