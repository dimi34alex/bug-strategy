using BugStrategy.Missions;
using BugStrategy.Unit;
using CycleFramework.Execute;
using Zenject;

namespace BugStrategy.EntityState.Unit
{
    public class UnitStateMachineUpdater : CycleInitializerBase
    {
        [Inject] private MissionData _missionData;
    
        private UnitRepository _unitRepository;

        protected override void OnInit()
        {
            _unitRepository = _missionData.UnitRepository;
        }
        protected override void OnUpdate()
        {
            foreach (var unitList in _unitRepository.Units)
            {
                foreach (var unit in unitList.Value)
                {
                    unit.StateMachine.OnUpdate();
                }
            }
        }
    }
}
