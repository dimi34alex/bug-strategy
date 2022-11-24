using UnityEngine;

public class BeesWasProducer : CycleInitializerBase
{
    protected override void OnInit()
    {
        FrameworkCommander.GlobalData.ConstructionSelector.OnSelectionChange += OnSelectionChange;
    }

    protected override void OnUpdate()
    {

    }

    private void OnSelectionChange()
    {
        
    }
}
