using UnityEngine;
using Zenject;

namespace Source.Scripts.Ai
{
    public class AiUpdater : CycleInitializerBase
    {
        [Inject] private AisProvider _beesGlobalAi;
        
        protected override void OnUpdate() 
            => _beesGlobalAi.ManualUpdate(Time.deltaTime);
    }
}