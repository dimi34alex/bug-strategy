using System;

[Serializable]
public class GlobalData
{
    public UnitRepository UnitRepository;
    public ResourceRepository ResourceRepository;
    public ConstructionsRepository ConstructionsRepository;
    public GlobalData()
    {
        UnitRepository = new UnitRepository();
        ConstructionsRepository = new ConstructionsRepository();
    }
}
