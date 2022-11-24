using System;

[Serializable]
public class GlobalData
{
    public UnitRepository UnitRepository;
    public ResourceRepository ResourceRepository;
    public ConstructionsRepository ConstructionsRepository;
    public ConstructionSelector ConstructionSelector;
    public GlobalData()
    {
        UnitRepository = new UnitRepository();
        ConstructionsRepository = new ConstructionsRepository();
        ConstructionSelector = new ConstructionSelector(ConstructionsRepository);
    }
}
