using UnityEngine;

namespace Unit
{
    public class UnitsUpdater : CycleInitializerBase
    {
        protected override void OnUpdate()
        {
            var time = Time.deltaTime;
            foreach (var units in FrameworkCommander.GlobalData.UnitRepository.Units)
                foreach (var unit in units.Value)
                    unit.HandleUpdate(time);
        }
    }
}