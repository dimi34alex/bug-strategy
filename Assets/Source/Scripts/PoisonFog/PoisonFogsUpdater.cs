using System.Linq;
using BugStrategy.Missions;
using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.PoisonFog
{
    public class PoisonFogsUpdater : CycleInitializerBase
    {
        [Inject] private MissionData _missionData;
        
        protected override void OnUpdate()
        {
            var time = Time.deltaTime;
            var fogs = _missionData.PoisonFogsRepository.Fogs.ToList();
            foreach (var fog in fogs)
                fog.HandleUpdate(time);
        }
    }
}