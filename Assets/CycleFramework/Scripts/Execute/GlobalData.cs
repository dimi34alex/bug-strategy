using System;
using Projectiles;

[Serializable]
public class GlobalData
{
    public readonly UnitRepository UnitRepository;
    public readonly ProjectilesRepository ProjectilesRepository;
    public readonly ResourceRepository ResourceRepository;
    public readonly ConstructionsRepository ConstructionsRepository;
    public readonly ConstructionSelector ConstructionSelector;
    
    public GlobalData()
    {
        UnitRepository = new UnitRepository();
        ProjectilesRepository = new ProjectilesRepository();
        ResourceRepository = new ResourceRepository();
        ConstructionsRepository = new ConstructionsRepository();
        ConstructionSelector = new ConstructionSelector(ConstructionsRepository);
    }
}
