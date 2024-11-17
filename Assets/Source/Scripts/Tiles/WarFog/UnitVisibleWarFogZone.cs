using BugStrategy.Constructions;
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
        [Tooltip("скрипт юнита откуда можно выяснить принадлежность")]
        [SerializeField] private UnitBase ScriptWithUnitBase;

        [Inject] private readonly MissionData _missionData;

        private void Start ()
        {
            ScriptWithUnitBase = transform.GetComponentInParent<UnitBase>();
        }

        protected override void OnEnter (ITriggerable component)
        {
            if(ScriptWithUnitBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().AddWatcher();
            }
        } 

        protected override void OnExit (ITriggerable component)
        {
            if(ScriptWithUnitBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().RemoveWatcher();
            }
        }
    }
}
