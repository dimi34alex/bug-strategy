using BugStrategy.Missions;
using BugStrategy.Tiles;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BugStrategy
{
    public class UnitVisibleWarFogZone : VisibleWarFogZone
    {
        [Tooltip("скрипт юнита откуда можно вы€снить принадлежность")]
        public UnitBase ScriptWithUnitBase;

        [Inject] private readonly MissionData _missionData;

        protected override void GetAffiliationAdd (ITriggerable component)
        {
            if(ScriptWithUnitBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().AddWatcher();
            }
        } 
        protected override void GetAffiliationRemove (ITriggerable component)
        {
            if(ScriptWithUnitBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().RemoveWatcher();
            }
        }
    }
}
