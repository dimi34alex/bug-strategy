namespace Source.Scripts.UI
{
    public enum UnitTacticsType
    {
        Attack,
        Build,
        Repair,
    }

    public enum ConstructionProduct
    {
        AntSwoard,
        Ant,
    }

    public enum UnitActionsType
    {
        None = 0,
        Tactics = 10,
        Constructions = 20,
        Abilities = 30,
    }

    public enum ConstructionActionsType
    {
        None = 0,
        RecruitUnits = 10,
        ProduceResources = 20
    }
}