using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.Ai
{
    public class AiUpdater : CycleInitializerBase
    {
        [Inject] private AisProvider _beesGlobalAi;
        
        protected override void OnUpdate() 
            => _beesGlobalAi.ManualUpdate(Time.deltaTime);
    }
}