using System;
using BugStrategy.Constructions;
using BugStrategy.Missions;
using BugStrategy.Trigger;
using BugStrategy.Unit;
using CycleFramework.Extensions;
using UnityEngine;
using Zenject;

namespace BugStrategy.Tiles
{
    public class VisibleWarFogZone : TriggerZone
    {
        protected override Func<ITriggerable, bool> EnteredComponentIsSuitable => t => t is Tile;
        protected override bool _refreshEnteredComponentsAfterExit => false;      
    }
}