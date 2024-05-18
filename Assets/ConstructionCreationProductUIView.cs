public class ConstructionCreationProductUIView : ButtonPanelUIView<UnitType>
{
    private BarView _barView;

    private void Awake()
    {
        _barView = gameObject.GetComponentInChildren<BarView>();
    }

    public void InitBar(IReadOnlyResourceStorage storage)
    {
        _barView.Init(storage);
    }
}
