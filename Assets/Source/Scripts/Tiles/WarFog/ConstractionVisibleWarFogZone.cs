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
        [SerializeField] private ConstructionBase ScriptWithConstructionBase;
        
        [Inject] private readonly MissionData _missionData;

        private void Start ()
        {
            ScriptWithConstructionBase = transform.GetComponentInParent<ConstructionBase>();
        }
        protected override void OnEnter (ITriggerable component)
        {
            if(ScriptWithConstructionBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().AddWatcher();
            }
        }

        protected override void OnExit (ITriggerable component)
        {
            if(ScriptWithConstructionBase.Affiliation == _missionData.PlayerAffiliation)
            {
                component.Cast<Tile>().RemoveWatcher();
            }
        }
    }
}
