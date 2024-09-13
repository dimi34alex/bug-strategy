using System.Linq;
using Source.Scripts.Missions;
using UnityEngine;
using Zenject;

namespace PoisonFog
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