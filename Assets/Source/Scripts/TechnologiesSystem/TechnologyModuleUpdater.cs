using CycleFramework.Execute;
using UnityEngine;
using Zenject;

namespace BugStrategy.TechnologiesSystem
{
    public class TechnologyModuleUpdater : CycleInitializerBase
    {
        [Inject] private readonly TechnologyModule _technologyModule;
        
        protected override void OnUpdate()
        {
            _technologyModule.HandleUpdate(Time.deltaTime);
        }
    }
}