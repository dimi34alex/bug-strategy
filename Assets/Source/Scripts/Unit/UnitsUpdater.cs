using System.Linq;
using BugStrategy.Missions;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Unit
{
    public class UnitsUpdater : CycleInitializerBase
    {
        [Inject] private MissionData _missionData;
        
        protected override void OnUpdate()
        {
            var time = Time.deltaTime;
            foreach (var unitsPair in _missionData.UnitRepository.Units)
            {
                var units = unitsPair.Value.ToList();
                foreach (var unit in units)
                    unit.HandleUpdate(time);
            }
        }
    }
}