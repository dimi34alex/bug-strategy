using System;

[Serializable]
public class GlobalData
{
    public UnitRepository UnitRepository;
    public GlobalData()
    {
        UnitRepository = new UnitRepository();
    }
}
