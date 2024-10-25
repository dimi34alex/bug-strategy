using BugStrategy.Constructions;
using BugStrategy.Missions;
using BugStrategy.Tiles;
using BugStrategy.Trigger;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy
{
    public class ConstractionVisibleWarFogZone : VisibleWarFogZone
    {
        [Tooltip("скрипт юнита откуда можно вы€снить принадлежность")]
        public ConstructionBase ScriptWithConstructionBase;
        
        [Inject] private readonly MissionData _missionData;

        protected override void GetAffiliationAdd (ITriggerable component)
        {
            if(ScriptWithConstructionBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().AddWatcher();
            }
        }
        protected override void GetAffiliationRemove (ITriggerable component)
        {
            if(ScriptWithConstructionBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().RemoveWatcher();
            }
        }
    }
}
