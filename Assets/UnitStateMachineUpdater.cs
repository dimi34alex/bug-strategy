using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStateMachineUpdater : CycleInitializerBase
{
    private UnitRepository _unitRepository;

    protected override void OnInit()
    {
        _unitRepository = FrameworkCommander.GlobalData.UnitRepository;
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
