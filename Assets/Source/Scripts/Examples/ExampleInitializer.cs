using UnityEngine;

public class ExampleInitializer : CycleInitializerBase
{
    private SomeComponent _someComponent;
    private SomeUIScreen _someUIScreen;

    protected override void OnInit()
    {
        _someComponent = FindObjectOfType<SomeComponent>(true);
        _someUIScreen = UIScreenRepository.GetScreen<SomeUIScreen>();

        FrameworkCommander.GlobalData.SomeGlobalData.SomeInfo = "example info";
    }

    protected override void OnUpdate()
    {
        if (_someComponent != null)
            _someComponent.transform.position += new Vector3(0f, 0f, 1f * Time.deltaTime);

        _someUIScreen.SomeText.text = $"{Time.time.ToString("F2")}";
    }

    protected override void OnFixedUpdate()
    {

    }
}
