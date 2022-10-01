using System;

[Serializable]
public class GlobalData
{
    public SomeGlobalData SomeGlobalData;

    public GlobalData()
    {
        SomeGlobalData = new SomeGlobalData();
    }
}
