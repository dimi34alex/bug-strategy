using System;
using BugStrategy.Trigger;
using CycleFramework.Extensions;

namespace BugStrategy.Tiles
{
    public class VisibleWarFogZone : TriggerZone
    {
        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is Tile;
        protected override bool _refreshEnteredComponentsAfterExit => false;
    
        protected override void OnEnter(ITriggerable component)
        {
            component.Cast<Tile>().AddWatcher();
        }

        protected override void OnExit(ITriggerable component)
        {
            component.Cast<Tile>().RemoveWatcher();
        }
    }
}