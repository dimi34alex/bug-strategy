using System;
using BugStrategy.Constructions;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;

namespace BugStrategy.Tiles
{
    public class VisibleWarFogZone : TriggerZone
    {
        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is Tile;
        protected override bool _refreshEnteredComponentsAfterExit => false;

        public UnitBase ScriptWithUnitBase;//скрипт юнита откуда можно вы€снить принадлежность, может быть равен null
        public ConstructionBase ScriptWithConstractionBase;//скрипт здани€ откуда можно вы€снить принадлежность, может быть равен null

        protected override void OnEnter (ITriggerable component)
        {

            if(ScriptWithUnitBase != null)
            {
                if(ScriptWithUnitBase.Affiliation == AffiliationEnum.Team1)
                {
                    //Debug.Log(this.transform.parent.name);
                    component.Cast<Tile>().AddWatcher();
                }
            }
            else
            {
                if(ScriptWithConstractionBase != null)
                {
                    if(ScriptWithConstractionBase.Affiliation == AffiliationEnum.Team1)
                    {
                        //Debug.Log(this.transform.parent.name);
                        component.Cast<Tile>().AddWatcher();
                    }
                }
            }
        }

        protected override void OnExit (ITriggerable component)
        {
            if(ScriptWithUnitBase != null)
            {
                if(ScriptWithUnitBase.Affiliation == AffiliationEnum.Team1)
                {
                    component.Cast<Tile>().RemoveWatcher();
                }
            }
            else
            {
                if(ScriptWithConstractionBase != null)
                {
                    if(ScriptWithConstractionBase.Affiliation == AffiliationEnum.Team1)
                    {
                        component.Cast<Tile>().RemoveWatcher();
                    }
                }
            }
        }
    }
}